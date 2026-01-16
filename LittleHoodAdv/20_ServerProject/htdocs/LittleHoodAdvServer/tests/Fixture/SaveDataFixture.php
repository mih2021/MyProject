<?php
declare(strict_types=1);

namespace App\Test\Fixture;

use Cake\TestSuite\Fixture\TestFixture;

/**
 * SaveDataFixture
 */
class SaveDataFixture extends TestFixture
{
    /**
     * Init method
     *
     * @return void
     */
    public function init(): void
    {
        $this->records = [
            [
                'id' => 1,
                'user_id' => 1,
                'stage_type' => 'Lorem ipsum dolor sit amet',
                'clear_time_h' => 1,
                'clear_time_m' => 1,
                'clear_time_s' => 1,
                'clear_time_original' => 'Lorem ipsum dolor sit amet',
                'created_at' => '2026-01-09 07:03:19',
            ],
        ];
        parent::init();
    }
}
