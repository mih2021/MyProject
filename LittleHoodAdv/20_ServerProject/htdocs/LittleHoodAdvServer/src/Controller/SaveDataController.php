<?php
declare(strict_types=1);

namespace App\Controller;

/**
 * SaveData Controller
 *
 * @property \App\Model\Table\SaveDataTable $SaveData
 */
class SaveDataController extends AppController
{
    /**
     * Index method
     *
     * @return \Cake\Http\Response|null|void Renders view
     */
    public function index()
    {
        $query = $this->SaveData->find()
            ->contain(['Users']);
        $saveData = $this->paginate($query);

        $this->set(compact('saveData'));
    }

    /**
     * View method
     *
     * @param string|null $id Save Data id.
     * @return \Cake\Http\Response|null|void Renders view
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function view($id = null)
    {
        $saveData = $this->SaveData->get($id, contain: ['Users']);
        $this->set(compact('saveData'));
    }

    /**
     * Add method
     *
     * @return \Cake\Http\Response|null|void Redirects on successful add, renders view otherwise.
     */
    public function add()
    {
        $saveData = $this->SaveData->newEmptyEntity();
        if ($this->request->is('post')) {
            $saveData = $this->SaveData->patchEntity($saveData, $this->request->getData());
            if ($this->SaveData->save($saveData)) {
                $this->Flash->success(__('The save data has been saved.'));

                return $this->redirect(['action' => 'index']);
            }
            $this->Flash->error(__('The save data could not be saved. Please, try again.'));
        }
        $users = $this->SaveData->Users->find('list', limit: 200)->all();
        $this->set(compact('saveData', 'users'));
    }

    /**
     * Edit method
     *
     * @param string|null $id Save Data id.
     * @return \Cake\Http\Response|null|void Redirects on successful edit, renders view otherwise.
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function edit($id = null)
    {
        $saveData = $this->SaveData->get($id, contain: []);
        if ($this->request->is(['patch', 'post', 'put'])) {
            $saveData = $this->SaveData->patchEntity($saveData, $this->request->getData());
            if ($this->SaveData->save($saveData)) {
                $this->Flash->success(__('The save data has been saved.'));

                return $this->redirect(['action' => 'index']);
            }
            $this->Flash->error(__('The save data could not be saved. Please, try again.'));
        }
        $users = $this->SaveData->Users->find('list', limit: 200)->all();
        $this->set(compact('saveData', 'users'));
    }

    /**
     * Delete method
     *
     * @param string|null $id Save Data id.
     * @return \Cake\Http\Response|null Redirects to index.
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function delete($id = null)
    {
        $this->request->allowMethod(['post', 'delete']);
        $saveData = $this->SaveData->get($id);
        if ($this->SaveData->delete($saveData)) {
            $this->Flash->success(__('The save data has been deleted.'));
        } else {
            $this->Flash->error(__('The save data could not be deleted. Please, try again.'));
        }

        return $this->redirect(['action' => 'index']);
    }
}
