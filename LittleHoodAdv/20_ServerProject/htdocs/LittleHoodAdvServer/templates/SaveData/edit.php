<?php
/**
 * @var \App\View\AppView $this
 * @var \App\Model\Entity\SaveData $saveData
 * @var string[]|\Cake\Collection\CollectionInterface $users
 */
?>
<div class="row">
    <aside class="column">
        <div class="side-nav">
            <h4 class="heading"><?= __('Actions') ?></h4>
            <?= $this->Form->postLink(
                __('Delete'),
                ['action' => 'delete', $saveData->id],
                ['confirm' => __('Are you sure you want to delete # {0}?', $saveData->id), 'class' => 'side-nav-item']
            ) ?>
            <?= $this->Html->link(__('List Save Data'), ['action' => 'index'], ['class' => 'side-nav-item']) ?>
        </div>
    </aside>
    <div class="column column-80">
        <div class="saveData form content">
            <?= $this->Form->create($saveData) ?>
            <fieldset>
                <legend><?= __('Edit Save Data') ?></legend>
                <?php
                    echo $this->Form->control('user_id', ['options' => $users]);
                    echo $this->Form->control('stage_type');
                    echo $this->Form->control('clear_time_h');
                    echo $this->Form->control('clear_time_m');
                    echo $this->Form->control('clear_time_s');
                    echo $this->Form->control('clear_time_original');
                    echo $this->Form->control('created_at');
                ?>
            </fieldset>
            <?= $this->Form->button(__('Submit')) ?>
            <?= $this->Form->end() ?>
        </div>
    </div>
</div>
