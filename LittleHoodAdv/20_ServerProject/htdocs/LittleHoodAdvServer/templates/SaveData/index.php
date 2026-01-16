<?php
/**
 * @var \App\View\AppView $this
 * @var iterable<\App\Model\Entity\SaveData> $saveData
 */
?>
<div class="saveData index content">
    <?= $this->Html->link(__('New Save Data'), ['action' => 'add'], ['class' => 'button float-right']) ?>
    <h3><?= __('Save Data') ?></h3>
    <div class="table-responsive">
        <table>
            <thead>
                <tr>
                    <th><?= $this->Paginator->sort('id') ?></th>
                    <th><?= $this->Paginator->sort('user_id') ?></th>
                    <th><?= $this->Paginator->sort('stage_type') ?></th>
                    <th><?= $this->Paginator->sort('clear_time_h') ?></th>
                    <th><?= $this->Paginator->sort('clear_time_m') ?></th>
                    <th><?= $this->Paginator->sort('clear_time_s') ?></th>
                    <th><?= $this->Paginator->sort('clear_time_original') ?></th>
                    <th><?= $this->Paginator->sort('created_at') ?></th>
                    <th class="actions"><?= __('Actions') ?></th>
                </tr>
            </thead>
            <tbody>
                <?php foreach ($saveData as $saveData): ?>
                <tr>
                    <td><?= $this->Number->format($saveData->id) ?></td>
                    <td><?= $saveData->hasValue('user') ? $this->Html->link($saveData->user->user_name, ['controller' => 'Users', 'action' => 'view', $saveData->user->user_id]) : '' ?></td>
                    <td><?= h($saveData->stage_type) ?></td>
                    <td><?= $this->Number->format($saveData->clear_time_h) ?></td>
                    <td><?= $this->Number->format($saveData->clear_time_m) ?></td>
                    <td><?= $this->Number->format($saveData->clear_time_s) ?></td>
                    <td><?= h($saveData->clear_time_original) ?></td>
                    <td><?= h($saveData->created_at) ?></td>
                    <td class="actions">
                        <?= $this->Html->link(__('View'), ['action' => 'view', $saveData->id]) ?>
                        <?= $this->Html->link(__('Edit'), ['action' => 'edit', $saveData->id]) ?>
                        <?= $this->Form->postLink(
                            __('Delete'),
                            ['action' => 'delete', $saveData->id],
                            [
                                'method' => 'delete',
                                'confirm' => __('Are you sure you want to delete # {0}?', $saveData->id),
                            ]
                        ) ?>
                    </td>
                </tr>
                <?php endforeach; ?>
            </tbody>
        </table>
    </div>
    <div class="paginator">
        <ul class="pagination">
            <?= $this->Paginator->first('<< ' . __('first')) ?>
            <?= $this->Paginator->prev('< ' . __('previous')) ?>
            <?= $this->Paginator->numbers() ?>
            <?= $this->Paginator->next(__('next') . ' >') ?>
            <?= $this->Paginator->last(__('last') . ' >>') ?>
        </ul>
        <p><?= $this->Paginator->counter(__('Page {{page}} of {{pages}}, showing {{current}} record(s) out of {{count}} total')) ?></p>
    </div>
</div>