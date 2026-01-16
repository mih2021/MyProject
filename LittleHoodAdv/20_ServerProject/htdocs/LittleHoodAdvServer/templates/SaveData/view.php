<?php
/**
 * @var \App\View\AppView $this
 * @var \App\Model\Entity\SaveData $saveData
 */
?>
<div class="row">
    <aside class="column">
        <div class="side-nav">
            <h4 class="heading"><?= __('Actions') ?></h4>
            <?= $this->Html->link(__('Edit Save Data'), ['action' => 'edit', $saveData->id], ['class' => 'side-nav-item']) ?>
            <?= $this->Form->postLink(__('Delete Save Data'), ['action' => 'delete', $saveData->id], ['confirm' => __('Are you sure you want to delete # {0}?', $saveData->id), 'class' => 'side-nav-item']) ?>
            <?= $this->Html->link(__('List Save Data'), ['action' => 'index'], ['class' => 'side-nav-item']) ?>
            <?= $this->Html->link(__('New Save Data'), ['action' => 'add'], ['class' => 'side-nav-item']) ?>
        </div>
    </aside>
    <div class="column column-80">
        <div class="saveData view content">
            <h3><?= h($saveData->stage_type) ?></h3>
            <table>
                <tr>
                    <th><?= __('User') ?></th>
                    <td><?= $saveData->hasValue('user') ? $this->Html->link($saveData->user->user_name, ['controller' => 'Users', 'action' => 'view', $saveData->user->user_id]) : '' ?></td>
                </tr>
                <tr>
                    <th><?= __('Stage Type') ?></th>
                    <td><?= h($saveData->stage_type) ?></td>
                </tr>
                <tr>
                    <th><?= __('Clear Time Original') ?></th>
                    <td><?= h($saveData->clear_time_original) ?></td>
                </tr>
                <tr>
                    <th><?= __('Id') ?></th>
                    <td><?= $this->Number->format($saveData->id) ?></td>
                </tr>
                <tr>
                    <th><?= __('Clear Time H') ?></th>
                    <td><?= $this->Number->format($saveData->clear_time_h) ?></td>
                </tr>
                <tr>
                    <th><?= __('Clear Time M') ?></th>
                    <td><?= $this->Number->format($saveData->clear_time_m) ?></td>
                </tr>
                <tr>
                    <th><?= __('Clear Time S') ?></th>
                    <td><?= $this->Number->format($saveData->clear_time_s) ?></td>
                </tr>
                <tr>
                    <th><?= __('Created At') ?></th>
                    <td><?= h($saveData->created_at) ?></td>
                </tr>
            </table>
        </div>
    </div>
</div>