<?php
declare(strict_types=1);

namespace App\Model\Table;

use Cake\ORM\Query\SelectQuery;
use Cake\ORM\RulesChecker;
use Cake\ORM\Table;
use Cake\Validation\Validator;

/**
 * SaveData Model
 *
 * @property \App\Model\Table\UsersTable&\Cake\ORM\Association\BelongsTo $Users
 *
 * @method \App\Model\Entity\SaveData newEmptyEntity()
 * @method \App\Model\Entity\SaveData newEntity(array $data, array $options = [])
 * @method array<\App\Model\Entity\SaveData> newEntities(array $data, array $options = [])
 * @method \App\Model\Entity\SaveData get(mixed $primaryKey, array|string $finder = 'all', \Psr\SimpleCache\CacheInterface|string|null $cache = null, \Closure|string|null $cacheKey = null, mixed ...$args)
 * @method \App\Model\Entity\SaveData findOrCreate($search, ?callable $callback = null, array $options = [])
 * @method \App\Model\Entity\SaveData patchEntity(\Cake\Datasource\EntityInterface $entity, array $data, array $options = [])
 * @method array<\App\Model\Entity\SaveData> patchEntities(iterable $entities, array $data, array $options = [])
 * @method \App\Model\Entity\SaveData|false save(\Cake\Datasource\EntityInterface $entity, array $options = [])
 * @method \App\Model\Entity\SaveData saveOrFail(\Cake\Datasource\EntityInterface $entity, array $options = [])
 * @method iterable<\App\Model\Entity\SaveData>|\Cake\Datasource\ResultSetInterface<\App\Model\Entity\SaveData>|false saveMany(iterable $entities, array $options = [])
 * @method iterable<\App\Model\Entity\SaveData>|\Cake\Datasource\ResultSetInterface<\App\Model\Entity\SaveData> saveManyOrFail(iterable $entities, array $options = [])
 * @method iterable<\App\Model\Entity\SaveData>|\Cake\Datasource\ResultSetInterface<\App\Model\Entity\SaveData>|false deleteMany(iterable $entities, array $options = [])
 * @method iterable<\App\Model\Entity\SaveData>|\Cake\Datasource\ResultSetInterface<\App\Model\Entity\SaveData> deleteManyOrFail(iterable $entities, array $options = [])
 */
class SaveDataTable extends Table
{
    /**
     * Initialize method
     *
     * @param array<string, mixed> $config The configuration for the Table.
     * @return void
     */
    public function initialize(array $config): void
    {
        parent::initialize($config);

        $this->setTable('save_data');
        $this->setDisplayField('stage_type');
        $this->setPrimaryKey('id');

        $this->belongsTo('Users', [
            'foreignKey' => 'user_id',
            'joinType' => 'INNER',
        ]);
    }

    /**
     * Default validation rules.
     *
     * @param \Cake\Validation\Validator $validator Validator instance.
     * @return \Cake\Validation\Validator
     */
    public function validationDefault(Validator $validator): Validator
    {
        $validator
            ->integer('user_id')
            ->notEmptyString('user_id');

        $validator
            ->scalar('stage_type')
            ->maxLength('stage_type', 50)
            ->requirePresence('stage_type', 'create')
            ->notEmptyString('stage_type');

        $validator
            ->integer('clear_time_h')
            ->requirePresence('clear_time_h', 'create')
            ->notEmptyString('clear_time_h');

        $validator
            ->integer('clear_time_m')
            ->requirePresence('clear_time_m', 'create')
            ->notEmptyString('clear_time_m');

        $validator
            ->integer('clear_time_s')
            ->requirePresence('clear_time_s', 'create')
            ->notEmptyString('clear_time_s');

        $validator
            ->scalar('clear_time_original')
            ->maxLength('clear_time_original', 50)
            ->requirePresence('clear_time_original', 'create')
            ->notEmptyString('clear_time_original');

        $validator
            ->dateTime('created_at')
            ->notEmptyDateTime('created_at');

        return $validator;
    }

    /**
     * Returns a rules checker object that will be used for validating
     * application integrity.
     *
     * @param \Cake\ORM\RulesChecker $rules The rules object to be modified.
     * @return \Cake\ORM\RulesChecker
     */
    public function buildRules(RulesChecker $rules): RulesChecker
    {
        $rules->add($rules->existsIn(['user_id'], 'Users'), ['errorField' => 'user_id']);

        return $rules;
    }
}
