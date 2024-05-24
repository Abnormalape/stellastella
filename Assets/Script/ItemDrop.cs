using System;
using Unity;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
class ItemDrop : MonoBehaviour
{
	[SerializeField]
	int bounces = 2; // 몇번 뛸건가? 최소 1회는 뛰어야 아이템이 먹어짐
	[SerializeField]
	float bounceRadius = 1f; // 생성지점으로 부터 얼마나 퍼지는가?
	[SerializeField]
	float makeRadius = 0.3f; // 오브젝트 중앙으로 부터 얼마나 멀리서부터 생성되는가?
	[SerializeField]
	float jumpHeight = 2f; // 얼마나 높게 점프하는가?

	float trueBounce;
	float trueRadius;
	float moveSpeed;
	float bouncingtime=0;
	float timepassed = 0;
	Vector2 bounceVector; 
	Vector2 makeVector;
	Rigidbody2D itemBody;

	bool firstForce= true;
	bool secondForce = false;
	bool thirdForce = false;
	

	private void OnEnable()
    {
        GetComponent<CircleCollider2D>().isTrigger = false;
        trueBounce = Random.Range(-bounceRadius,bounceRadius); //랜덤하게 튀는 반경 생성
		trueRadius = Random.Range(-makeRadius,makeRadius); //랜덤하게 생성 반경 생성
		bounceVector = Random.insideUnitCircle;
		makeVector = Random.insideUnitCircle;
		this.transform.position = new Vector2(this.transform.position.x +(trueBounce * makeVector.x) , this.transform.position.y +(trueBounce * makeVector.y)); // 생성된 위치에서 makevector위치로 trueradius만큼 이동해서 생성
		moveSpeed = trueBounce * 2.5f;
		itemBody = GetComponent<Rigidbody2D>();
	}
    private void Update() // 생각한 거랑 좀 다르긴 한데 만족스러우니 넘어감
    {
		timepassed += Time.deltaTime;
		bouncingtime += Time.deltaTime;
		if (firstForce) // 일회성 속도
		{
			itemBody.velocity = new Vector2(bounceVector.x * moveSpeed ,bounceVector.y * moveSpeed + 1f)*2;
			firstForce = false;
			secondForce = true;
		}
		if(secondForce && bouncingtime >= 0.4f)
		{
			itemBody.velocity = new Vector2(bounceVector.x * moveSpeed, bounceVector.y * moveSpeed + 1f) * 0.6f;
			secondForce = false;
			thirdForce = true;
			bouncingtime = 0;
		}
		if(thirdForce && bouncingtime >= 0.15f)
		{
			itemBody.velocity = new Vector2(bounceVector.x * moveSpeed, bounceVector.y * moveSpeed + 1f) * 0.2f;
			thirdForce = false;
			bouncingtime = 0;
		}

		if (!firstForce && !secondForce && !thirdForce && bouncingtime >= 0.04f)
		{
			itemBody.velocity = Vector2.zero;
		}
		else
		{
			itemBody.velocity += new Vector2(0, -10f * Time.deltaTime);
		}

		if(timepassed > 1f)
		{
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
}