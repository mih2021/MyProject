<?php
declare(strict_types=1);

namespace App\Controller;

/**
 * Users Controller
 *
 * @property \App\Model\Table\UsersTable $Users
 */
class APIController extends AppController
{
    const STAGE_TYPE = ["tutorial", "stage1", "stage2", "stage3"];

    public function initialize(): void
    {
        parent::initialize();

        $this->Users = $this->fetchTable('Users');
        $this->SaveData = $this->fetchTable('SaveData');
    }
    /**
     * 入力されたユーザー名からユーザーIDを返却する
     * 
     * @param string $userName 入力されたユーザー名
     * @return json 入力されたユーザー名からユーザーIDを返却する
     */
    public function loginUser(){
        $this->autoRender = false;

        $userName = $this->request->getData("userName");

        $userId = $this->getUser($userName) ?? '';

        // user_id に紐づく stage_type を重複なしで取得

        $rows = $this->SaveData
                ->find()
                ->select(['stage_type'])
                ->where(['user_id' => $userId])
                ->where(['stage_type' => "tutorial"])
                ->first();
        

        $data = [
            'user_id' => $userId
            , 'tutorial' => $rows != null ? 1 : 0
        ];

        return $this->response
        ->withType('application/json')
        ->withStringBody(json_encode($data));
    }

    /**
     * 入力されたユーザー名でユーザーデータを作成する
     * 
     * @param string $userName 入力されたユーザー名
     * @return bool 成否
     */
    public function createUser(){
        error_log("createUser()");
        $this->autoRender = false;

        $userName = $this->request->getData("userName");

        $data = array( "user_name" => $userName );

        // 同じユーザー名があるか確認する
        $userId = $this->getUser($userName);
        if($userId != null){
            return $this->response
            ->withType('application/json')
            ->withStatus(200)
            ->withStringBody(json_encode([
                'success' => false
                , 'message' => 'This username already exists.'
        ]));
        }
        
        $userData = $this->Users->newEmptyEntity();
        $userData = $this->Users->patchEntity($userData, $data);

        if ($this->Users->save($userData)) {
        // 成功
        return $this->response
            ->withType('application/json')
            ->withStatus(200)
            ->withStringBody(json_encode([
                'success' => true
                , 'message' => ''
            ]));
        }

        // 失敗
        return $this->response
            ->withType('application/json')
            ->withStatus(400)
            ->withStringBody(json_encode([
                'success' => false
                , 'message' => 'Registration failed.'
        ]));
    }

    /**
     * 定数を取得する（ステージ名）
     * 
     * @param 
     * @return Json　ステージ名の列挙
     */
    public function getStageType(){
        return $this->response
        ->withType('application/json')
        ->withStringBody(json_encode([
            'stage_types' => self::STAGE_TYPE
        ]));
    }

    /**
     * ステージクリアのデータを保存する
     * 
     * @param userId
     * @param stageType
     * @param clearTime
     * @param createdAt
     * @return bool 成否
     */

    public function sendSaveData(){
        $this->autoRender = false;
        $userId = $this->request->getData("userId");
        $stageType = $this->request->getData("stageType");
        $clearTimeOriginal = $this->request->getData("clearTimeOriginal");
        $clearTimeH = $this->request->getData("clearTimeH");
        $clearTimeM = $this->request->getData("clearTimeM");
        $clearTimeS = $this->request->getData("clearTimeS");

        $data = array(
            "user_id" => $userId
            , "stage_type" => $stageType
            , "clear_time_original" => $clearTimeOriginal
            , "clear_time_h" => $clearTimeH
            , "clear_time_m" => $clearTimeM
            , "clear_time_s" => $clearTimeS
        );
        
        $saveDataData = $this->SaveData->newEmptyEntity();
        $saveDataData = $this->SaveData->patchEntity($saveDataData, $data);

        if ($this->SaveData->save($saveDataData)) {
            // 成功
            return $this->response
                ->withType('application/json')
                ->withStatus(200)
                ->withStringBody(json_encode([
                    'success' => true
                ]));
        }

        $this->log(print_r($saveDataData->getErrors(), true), LOG_DEBUG);

        // 失敗
        return $this->response
            ->withType('application/json')
            ->withStatus(400)
            ->withStringBody(json_encode([
                'success' => false
        ]));
    }

    /**
     * ユーザーのセーブデータを取得する
     * 
     * @param userId
     * @return 各ステージのクリアデータ
     */
    public function getSaveData(){
        $this->autoRender = false;

        $userId = $this->request->getData("userId");

        // 初期値（すべて0）
        $result = array_fill_keys(self::STAGE_TYPE, 0);

        // user_id に紐づく stage_type を重複なしで取得
        $rows = $this->SaveData
            ->find()
            ->select(['stage_type'])
            ->where(['user_id' => $userId])
            ->distinct(['stage_type'])
            ->all();

        // 取得できた stage_type を 1 にする
        foreach ($rows as $row) {
            if (isset($result[$row->stage_type])) {
                $result[$row->stage_type] = 1;
            }
        }

        return $this->response
            ->withType('application/json')
            ->withStringBody(json_encode($result));
        
    }

    public function getRank(){

        $this->autoRender = false;

        $stageType = $this->request->getData('stageType');

        $rows = $this->SaveData
            ->find()
            ->select([
                'Users.user_name',
                'SaveData.clear_time_h',
                'SaveData.clear_time_m',
                'SaveData.clear_time_s'
            ])
            ->contain(['Users'])
            ->where(['SaveData.stage_type' => $stageType])
            ->order(['SaveData.clear_time_h' => 'ASC'
            ,'SaveData.clear_time_m' => 'ASC'
            ,'SaveData.clear_time_s' => 'ASC'])
            ->limit(10)
            ->all();

        $result = [];
        foreach ($rows as $row) {
            $result[] = [
                'user_name'  => $row->user->user_name,
                'clear_time'=> sprintf(
                    '%02d:%02d:%02d',
                    $row->clear_time_h,
                    $row->clear_time_m,
                    $row->clear_time_s
                )
            ];
        }

        return $this->response
            ->withType('application/json')
            ->withStringBody(json_encode($result));
    }

    private function getUser($userName){
        $result = null;

        $query = $this->Users->find()->where(['user_name' => $userName]);
        $data = $query->first();
        
        if($data != null){
            $result = $data->user_id;
        }

        return $result;
    }
}
