# 스파르타 던전 탐험 만들기

Unity 3D에 대해 학습하고, Unity3D의 캐릭터 이동과 물리 처리를 직접 구현

---

### 기능 가이드

- Input System을 이용한 기본 이동 및 점프
  > Player Input Action에 각 키 값에 따른 Actions을 지정하고  
  > PlayerController.cs에서 각 키를 만족하는 Input이 들어오면 동작을 처리하도록 하였다.
  
  > 기본 이동에 Dash 기능을 추가하여, Shift 키를 누르는 동안 Stamina를 소비하여 속도를 낼 수 있도록 하였다.
  ![이미지 이름](이미지 링크 주소)

- 체력바 UI 표시
  > UI 캔버스에 체력바를 추가하고 Player의 체력이 나타나도록 설정하였다.
  > Player의 체력에 따라 UI가 갱신된다.

- Raycast를 활용한 동적 환경 조사
  > Raycast를 통해 지속적으로 아이템과 상호작용 할 수 있도록 하였다.
  > 아이템과 Raycast가 닿으면 아이템의 이름과 설명이 UI로 표시된다.

- Rigidbody ForceMode를 활용한 점프대
  > layer로 Player인지를 판별하고, Player가 맞다면 Player Rigidbody에 AddForce하여 위쪽으로 순간적인 힘을 가하였다.

- ScriptableObject를 활용한 아이템 데이터
  > 다양한 아이템 데이터를 ScriptableObject로 정의하고,
  > 각 아이템의 이름, 설명, 속성 등을 ScriptableObject로 관리하였다.

- Coroutine을 활용한 일정 시간 지속되는 아이템 능력
  > Coffee 아이템 사용 후 5초 동안 속도가 2배가 되도록 하였다.

- Stamina를 표시하는 추가 UI
  > Stamina UI를 추가하여 Dash 시 소모되는 Stamina를 사용자가 확인할 수 있도록 하였다.

- Coroutine을 활용한 움직이는 플랫폼
  > Coroutine과 Lerp를 이용하여 일정한 시간 동안 정해진 구역을 이동하는 MovingPad를 구현하였다.
  
  > OnTriggerEnter와 OnTriggerExit를 이용하여, 점프대 위에서 OnTrigger 이벤트가 발생하면
  > Player를 MovingPad Object의 자식으로 옮겨 MovingPad와 함께 이동하도록 하였다.

- Raycast를 활용한 벽 타기 및 매달리기
  >

- 다양한 아이템 구현
  >
