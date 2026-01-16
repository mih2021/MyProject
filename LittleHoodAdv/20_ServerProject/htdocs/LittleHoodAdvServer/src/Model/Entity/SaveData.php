<?php
declare(strict_types=1);

namespace App\Model\Entity;

use Cake\ORM\Entity;

/**
 * SaveData Entity
 *
 * @property int $id
 * @property int $user_id
 * @property string $stage_type
 * @property int $clear_time_h
 * @property int $clear_time_m
 * @property int $clear_time_s
 * @property string $clear_time_original
 * @property \Cake\I18n\DateTime $created_at
 *
 * @property \App\Model\Entity\User $user
 */
class SaveData extends Entity
{
    /**
     * Fields that can be mass assigned using newEntity() or patchEntity().
     *
     * Note that when '*' is set to true, this allows all unspecified fields to
     * be mass assigned. For security purposes, it is advised to set '*' to false
     * (or remove it), and explicitly make individual fields accessible as needed.
     *
     * @var array<string, bool>
     */
    protected array $_accessible = [
        'user_id' => true,
        'stage_type' => true,
        'clear_time_h' => true,
        'clear_time_m' => true,
        'clear_time_s' => true,
        'clear_time_original' => true,
        'created_at' => true,
        'user' => true,
    ];
}
