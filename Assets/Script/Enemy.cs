using UnityEngine;

/// <summary>
/// Enemy 1 — enemy dasar, hanya bergerak lurus ke bawah.
/// Semua logika umum sudah ada di BaseEnemy.
/// </summary>
public class Enemy : BaseEnemy
{
    // Stats di-override lewat Inspector (Header sudah ada di BaseEnemy).
    // Default: hp=10, scoreValue=10, moveSpeed=4f
    //
    // Tidak perlu kode tambahan karena behaviour Enemy1 = behaviour default BaseEnemy.
    // Tambahkan override OnStart() / OnUpdate() / OnDeath() di sini jika suatu saat
    // Enemy1 perlu behaviour khusus.
}
