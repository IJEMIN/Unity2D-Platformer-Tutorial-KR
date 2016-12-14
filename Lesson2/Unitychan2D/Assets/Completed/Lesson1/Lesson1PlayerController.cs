using System.Collections;
using UnityEngine;

/*
 * 이 스크립트는
 * 1. 플레이어가 캐릭터를 조작하게 해줍니다.
 * 2. 바닥과의 충돌을 감지해줍니다.
 * 3. 알맞은 이동 애니메이션을 재생해줍니다.
 * 4. 적과의 충돌을 감지해줍니다.
 */

// [RequireComponent(typeof(컴포넌트_이름))] 는 해당 컴포넌트를 꼭 사용해야 한다고 알리는 태그
[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Lesson1PlayerController : MonoBehaviour
{
    // 내가 이동할 수 있는 최대 속도
    public float maxSpeed = 10f;

    // 나의 점프의 힘
    public float jumpPower = 1000f;

    // 내가 적과 부딫쳤을 때 x,y 방향으로 얼만큼 튕겨 나갈건지
    public Vector2 backwardForce = new Vector2(-4.5f, 5.4f);

    // 어떤 레이어가 땅인지 지정해줍니다
    public LayerMask whatIsGround;

    // 나에게 붙은 애니메이터, 박스 컬라이더2D(충돌체), 리기드바디2D 를 참조하기 위한 변수들
    private Animator m_animator;
    private BoxCollider2D m_boxcollier2D;
    private Rigidbody2D m_rigidbody2D;

    // 지금 내가 땅에 붙어 있는지
    private bool m_isGround;

    // 지금 내가 데미지를 입고 있는 상태인지
    // 만약 데미지를 입고 있는 상태라면 스턴에 걸려 조작을 막을 것이다
	private bool m_isDamaged;

    // 데미지를 입은 상태라면 m_isDamaged를 언제
    // 데미지를 안입은 상태 m_isDamaged = true 바꿔줄건지
    // 그 시간을 초 로 저장하는 곳
	private float m_nextReleaseTime;

    private PlayerAudio myAudio;

    // 내 키의 절반 입니다 (=내 키의 중심)
    // 이걸 사용하는 이유는, 내 중심을 기준으로 한 위치에서 내 키의 절반 만큼 빼면
    // 그곳이 내 발바닥 위치를 알 수 있기 때문
    // 그래서 내 발바닥 위치에서 바닥과 겹쳤는지 체크하여 m_isGround 를 true나 false 로 지정할 수 있습니다
    private const float m_centerY = 1.5f;


    // 게임(씬)이 처음 실행될때 맨 처음 한번만 실행되는 코드들이 옵니다
    void Start()
    {
        // 게임을 시작하면서 조종하고 싶은 컴포넌트들을 가져옵니다
        m_animator = GetComponent<Animator>();
        m_boxcollier2D = GetComponent<BoxCollider2D>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();

		// 나 자신의 박스형 충돌체의 사이즈와 보정위치를 수정합니다
		m_boxcollier2D.size = new Vector2(1, 2.5f);
		m_boxcollier2D.offset = new Vector2(0, -0.25f);
    }

    // 게임이 실행되는 동안 반복해서 (1초에 대략 30~60번) 동작하는 코드들이 옵니다
    void Update()
    {
		if (!m_isDamaged)
		{
            // 데미지를 입는 도중이 아니어야 조작이 가능합니다

            // 수평 방향 입력을 받습니다
			float x = Input.GetAxis("Horizontal");
            // 점프 입력을 받습니다
			bool jump = Input.GetButtonDown("Jump");
            // Move 함수에 입력받은 이동력(x)과 점프 여부(jump)를 입력으로 주어 호출합니다
			Move(x, jump);
		}
		else if (m_nextReleaseTime < Time.time && m_isGround) // 만약 데미지를 입는 도중인데, m_nextReleaseTime 시점을 지나서 이제 회복해도 된다면
		{
            // 이제는 데미지를 입은 상태를 풀어줍니다
			m_isDamaged = false;

			m_animator.Play("Idle");
		}
    }

    // 캐릭터의 움직임을 제어하는 매서드 입니다.
    // 참고* 매서드는 클래스 내부에 있는 함수로서, 함수와 매서드라는 용어를 크게 구별하여 사용하진 않습니다.
    void Move(float move, bool jump)
    {
        // 만약 입력으로 들어온 움직임이 마이너스 방향이면 캐릭터를 (y축 기준) 180도 돌려 뒤집습니다.
		if (move > 0)
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		else if (move < 0)
			transform.localRotation = Quaternion.Euler(0, 180, 0);

        // Rigidbody2D 를 이용하여 속도를 지정해 줍니다. y 방향의 속도는 수정하지 않고 현재 속도를 유지해줍니다.
        m_rigidbody2D.velocity = new Vector2(move * maxSpeed, m_rigidbody2D.velocity.y);

        // 미리 정의해둔 애니메이터의 파라미터에 값을 전달해줍니다.
        // 그러면 애니메이터가 알아서 조합된 값의 조건에 맞는 애니메이션을 선택해서 재생합니다
        m_animator.SetFloat("Horizontal", move);
        m_animator.SetFloat("Vertical", m_rigidbody2D.velocity.y);


        m_animator.SetBool("isGround", m_isGround);

        // 점프 부분입니다
        if (jump && m_isGround)
        {
            // if 조건문을 통해 오직 jump 버튼을 눌렀고
            // 지금 바닥에 붙어 있는 상태 m_isGround
            // 모두가 true(참) 인 경우에만 점프를 하는 코드가 실행됩니다

            // 애니메이터의 파라미터에 값을 전달합니다.
            // 트리거Trigger의 경우 SetTrigger 를 하면 방아쇠를 당겨 총을 쏘는 것 처럼
            // 한순간 true가 되었다가 바로 false 가 됩니다. 따라서 true나 false 값을 따로 지정해 주지 않아도 됩니다

            m_animator.SetTrigger("Jump");
            // 위쪽 방향으로 jumpPower 만큼 힘을 줍니다 ( Vector2.up 은 new Vector2(0,1) 과 같습니다 x 방향 0, y 방향 1)
            m_rigidbody2D.AddForce(Vector2.up * jumpPower);
        }
    }

    // FixedUpdate 는 Update 와 비슷하지만 좀더 빽빽하고 칼같이 일정한 주기로 실행 됩니다
    void FixedUpdate()
    {
        // 현재 위치를 가져옵니다
        Vector2 pos = transform.position;

        // 발바닥의 위치 = 내 위치 - 내 키의 절반
        Vector2 groundCheck = new Vector2(pos.x, pos.y - m_centerY);

        // 발바닥을 기준으로 체크할 넓이 영역을 지정합니다.
        // 여기서는 가로 두께는 나의 충돌체 보다 살짝 작게, 세로 두께는 0.10 으로 했습니다.
        Vector2 groundArea = new Vector2(m_boxcollier2D.size.x * 0.49f, 0.05f);


        // Physics2D.OverlapArea(첫번째 점, 두번째 점, 검사를 생략할 레이어들)
        // 이 함수는 첫번째 점을 좌측 상단, 두번째 점을 우측 하단으로 삼은 사각형과 나 자신이 서로 겹쳐 있는지 체크해서 true 나 false 를 건내줍니다
        // 세번째는 어떤 레이어를 검사에서 생략할지 안할지를 지정해줍니다. 여기에 whatIsGround 를 넣어, Ground 레이어를 제외한 다른 모든 레이어에 대한 체크는 생략합니다

        m_isGround = Physics2D.OverlapArea(groundCheck + groundArea, groundCheck - groundArea, whatIsGround);
        // m_isGround 를 그대로 넣어, 애니메이터에 미리 지정해둔 isGround 라는 파라미터를 지정해줍니다
        m_animator.SetBool("isGround", m_isGround);

    }

    // OnTrigger~ 함수는 Trigger 로 지정된 충돌체와 충돌했을때 호출됩니다
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "DamageObject" && !m_isDamaged)
        {
			m_isDamaged = true;
			m_nextReleaseTime = Time.time + 0.2f;
            OnDamage();
        }
    }

    // 데미지를 입었을 때 호출할 코드들 입니다
	void OnDamage()
	{
        // 땅에 있었다면
        if (m_isGround)
            m_animator.Play("Damage"); // 데미지를 입는 애니메이션을 바로 재생하고
        else
            m_animator.Play("AirDamage"); // 땅에 있지 않았다면, 공중에서 데미지를 입는 애니메이션을 재생합니다

        // 그리고 미리 지정해둔 backwardForce 만큼 튕겨나갑니다
		m_rigidbody2D.velocity = new Vector2(transform.right.x * backwardForce.x, transform.up.y * backwardForce.y);
    }

}
