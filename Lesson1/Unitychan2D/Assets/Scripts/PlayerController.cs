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
public class PlayerController : MonoBehaviour
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
    //private Animator m_animator;
    //private BoxCollider2D m_boxcollier2D;
    //private Rigidbody2D m_rigidbody2D;

    // 지금 내가 땅에 붙어 있는지
    private bool m_isGround;

    // 지금 내가 데미지를 입고 있는 상태인지
    // 만약 데미지를 입고 있는 상태라면 스턴에 걸려 조작을 막을 것이다
	private bool m_isDamaged;


	/*
    // 게임(씬)이 처음 실행될때 맨 처음 한번만 실행되는 코드들이 옵니다
    void Start()
    {
        // 게임을 시작하면서 조종하고 싶은 컴포넌트들을 가져옵니다


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

		}

    }

    // 캐릭터의 움직임을 제어하는 매서드 입니다.
    void Move(float move, bool jump)
    {
        
    }

    // FixedUpdate 는 Update 와 비슷하지만 좀더 빽빽하고 칼같이 일정한 주기로 실행 됩니다
    void FixedUpdate()
    {


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
	*/

}
