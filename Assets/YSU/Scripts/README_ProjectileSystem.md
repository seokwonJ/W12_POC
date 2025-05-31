# 투사체 발사 시스템 (Projectile Launch System)

## 개요
이 시스템은 Unity에서 랜덤한 방향으로 투사체를 발사할 수 있는 완전한 시스템입니다.

## 포함된 스크립트

### 1. ProjectileLauncher.cs
메인 발사기 스크립트로, 다음 기능을 제공합니다:
- 랜덤한 방향으로 투사체 발사
- 수동 발사 (스페이스바)
- 자동 발사 모드
- 2D/3D 랜덤 방향 설정
- 각도 범위 제한

### 2. Projectile.cs
투사체 오브젝트 스크립트로, 다음 기능을 제공합니다:
- 충돌 감지 및 데미지 처리
- 시각 효과 (Trail, Particle)
- 타겟 레이어 필터링
- 충돌 시 이펙트 생성

### 3. Health.cs (Projectile.cs에 포함)
데미지 시스템을 위한 헬스 컴포넌트입니다.

## 설정 방법

### 1. 발사기 설정
1. 빈 GameObject를 생성하고 `ProjectileLauncher` 스크립트를 추가
2. Inspector에서 다음 설정:
   - **Projectile Prefab**: 발사할 투사체 프리팹
   - **Fire Point**: 발사 위치 (없으면 자동으로 현재 오브젝트 위치 사용)
   - **Projectile Speed**: 투사체 속도
   - **Projectile Life Time**: 투사체 생존 시간

### 2. 랜덤 방향 설정
- **Use Random Direction**: 랜덤 방향 활성화
- **Min/Max Angle**: 랜덤 각도 범위 (-45° ~ 45°)
- **Randomize 3D**: 3D 공간에서 완전 랜덤 방향

### 3. 자동 발사 설정
- **Auto Fire**: 자동 발사 활성화
- **Fire Rate**: 초당 발사 횟수

### 4. 투사체 프리팹 생성
1. 기본 도형 (Sphere, Capsule 등) 생성
2. `Projectile` 스크립트 추가
3. `Rigidbody` 컴포넌트 추가
4. `Collider` 설정 (IsTrigger 또는 일반 충돌)
5. 선택사항: `TrailRenderer`, `ParticleSystem` 추가

## 사용 방법

### 기본 사용
```csharp
// 스페이스바로 수동 발사
// 또는 자동 발사 모드 활성화
```

### 스크립트에서 호출
```csharp
// 다른 스크립트에서 발사
ProjectileLauncher launcher = GetComponent<ProjectileLauncher>();

// 기본 발사
launcher.FireProjectile();

// 특정 방향으로 발사
launcher.FireProjectileWithDirection(Vector3.forward);

// 타겟을 향해 발사
launcher.FireProjectileTowards(targetTransform);
```

### 데미지 시스템 연동
타겟 오브젝트에 `Health` 컴포넌트를 추가하면 투사체가 자동으로 데미지를 적용합니다.

```csharp
// 타겟 오브젝트에 Health 컴포넌트 추가
Health health = gameObject.AddComponent<Health>();
```

## 팁

1. **성능 최적화**: 투사체가 많이 생성되는 경우 Object Pooling 사용 권장
2. **시각 효과**: TrailRenderer와 ParticleSystem으로 더 멋진 효과 연출
3. **물리 설정**: Rigidbody의 Drag 값으로 투사체 비행 특성 조절
4. **레이어 설정**: 투사체가 특정 오브젝트만 타격하도록 레이어 설정

## 예제 설정값
- **Projectile Speed**: 10-20 (일반적인 속도)
- **Life Time**: 3-5초 (자동 정리)
- **Fire Rate**: 1-3 (초당 발사 횟수)
- **Angle Range**: -30° ~ 30° (적당한 랜덤성)

이 시스템을 사용하여 다양한 게임에서 투사체 발사 메커니즘을 구현할 수 있습니다! 