/* 
Tree.js
b1018194 Ito Hajime
パスTree/以下の管理
*/
// Import express
const express = require('express')
// Import bodyParser
const bodyParser = require('body-parser')
const app = express()

app.use(bodyParser.urlencoded({ extended: true }))
app.use(bodyParser.json())

const router = express.Router()

// middleware that is specific to this router
router.use(function Auth(req, res, next) {
    //login確認処理
    /*const token = req.headers// headerのlogin tokenを受け取る
        (async () => {
            try {
                const decodedToken = await admin.auth().verifyIdToken(token)
                console.log("login success")
                next()
            } catch (error) {
                console.error("login error")
            }
        })()
    */
    next()//test用
})

router.route('/')
    //TreeKey Map表示のためにGET
    .get((req, res) => {
        //TreeKey
        const ref = db.ref("/Tree")
        try {
            // ボディからTreeKeyを取得
            const TreeKey = req.body.TreeKey
            ref.child(TreeKey).once('value', (snapshot) => {
                const locationX = snapshot.val().locationX
                const locationY = snapshot.val().locationY
                const owner = snapshot.val().owner
                const point = snapshot.val().point
                const TreeKey = snapshot.val().TreeKey
                const obj = {
                    "locationX": locationX,
                    "locationY": locationY,
                    "owner": owner,
                    "point": point,
                    "TreeKey": TreeKey,
                }
                // jsonに変換
                const json = JSON.stringify(obj)
                // jsonを返す
                res.send(json)
            })
            //res.send("success")
        } catch (error) {
            res.send("error")
        }
    })
    // Treeを作成する
    .post((req, res) => {
        /*
        curlコマンド
        curl -v -H "Accept: application/json" -H "Content-type: application/json" -X POST -d "{\"owner\":\"email@email.com\",\"locationY\":\"app123\",\"locationX\":\"app123\"}"  http://localhost:5000/Tree
        */
        try {
            const ref = db.ref("/Tree")
            const TreeKey = ref.push().key

            //デバック用
            /*
            console.log(JSON.stringify(req.body))
            console.log((req.body.locationX))
            */

            // Databaseに保存
            ref.child(TreeKey).set({
                locationX: req.body.locationX,
                locationY: req.body.locationY,
                owner: req.body.owner,
                point: 0,
                TreeKey: TreeKey,
            })
            res.send("success")
        } catch (error) {
            res.send("error")
        }
    })
    // Treeのポイントを追加
    .put((req, res) => {
        //TreeKeyを貰う
        try {
            const ref = db.ref("/Tree")
            ref.child(req.body.TreeKey + "/point").transaction(function (point) {
                return (point || 0) + 1
            })
            res.send("success")
        } catch (error) { res.send('error') }
    })

module.exports = router