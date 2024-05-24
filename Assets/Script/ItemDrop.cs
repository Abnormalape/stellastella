using System;
using Unity;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
class ItemDrop : MonoBehaviour
{
	[SerializeField]
	int bounces = 2; // ��� �۰ǰ�? �ּ� 1ȸ�� �پ�� �������� �Ծ���
	[SerializeField]
	float bounceRadius = 1f; // ������������ ���� �󸶳� �����°�?
	[SerializeField]
	float makeRadius = 0.3f; // ������Ʈ �߾����� ���� �󸶳� �ָ������� �����Ǵ°�?
	[SerializeField]
	float jumpHeight = 2f; // �󸶳� ���� �����ϴ°�?

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
        trueBounce = Random.Range(-bounceRadius,bounceRadius); //�����ϰ� Ƣ�� �ݰ� ����
		trueRadius = Random.Range(-makeRadius,makeRadius); //�����ϰ� ���� �ݰ� ����
		bounceVector = Random.insideUnitCircle;
		makeVector = Random.insideUnitCircle;
		this.transform.position = new Vector2(this.transform.position.x +(trueBounce * makeVector.x) , this.transform.position.y +(trueBounce * makeVector.y)); // ������ ��ġ���� makevector��ġ�� trueradius��ŭ �̵��ؼ� ����
		moveSpeed = trueBounce * 2.5f;
		itemBody = GetComponent<Rigidbody2D>();
	}
    private void Update() // ������ �Ŷ� �� �ٸ��� �ѵ� ����������� �Ѿ
    {
		timepassed += Time.deltaTime;
		bouncingtime += Time.deltaTime;
		if (firstForce) // ��ȸ�� �ӵ�
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