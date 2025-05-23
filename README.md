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
  > UI 캔버스에 체력바를 추가하고 Player의 체력이 나타나도록 설정하였다. Player의 체력에 따라 UI가 갱신된다.

- Raycast를 활용한 동적 환경 조사
  >
  >

- Rigidbody ForceMode를 활용한 점프대

- ScriptableObject를 활용한 아이템 데이터

- Coroutine을 활용한 일정 시간 지속되는 아이템 능력

- Stamina를 표시하는 추가 UI

- Coroutine을 활용한 움직이는 플랫폼

- Raycast를 활용한 벽 타기 및 매달리기

- 다양한 아이템 구현
- 
