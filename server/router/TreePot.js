/*
b1018194 Ito Hajime
TreePot.js
植木鉢セッションの管理
*/

// Import express
const express = require('express')
// Import bodyParser
const bodyParser = require('body-parser')
// Import distance.js
const distancejs = require("../js/distance")

const app = express()

app.use(bodyParser.urlencoded({ extended: true }))
app.use(bodyParser.json())

const router = express.Router()

let objects = [] // JSONを返す際の連想配列を格納する配列

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

    // 過去にuserの作成したTreePot,TreeのTreeKey,location,近いかどうかを返す
    .get((req, res) => {
        /*
        REQ JSON
        {
        pid: "XXXX"
        locationX: "XXXX"
        locationY: "XXXX"
        distance: "XXXX"
        }
        */
        try {
            const reqlocationX = req.body.locationX
            const reqlocationY = req.body.locationY
            const reqdistance = req.body.distance //一定距離
            const pid = req.body.pid
            //自分の作成したTreePotのパス
            let ref = db.ref('/Tree').orderByChild("owner").equalTo(pid)
            // 一定距離内のTreePot情報を格納する
            ref.once('value', (snapshot) => {
                const locationX = snapshot.val().locationX
                const locationY = snapshot.val().locationY
                const TreeKey = snapshot.val().TreeKey

                if (distancejs.distance(reqlocationX, reqlocationY, locationX, locationY, reqdistance)) {
                    //一定距離の中に入っている自分の作成したTreePot
                    const snap = {
                        "TreeKey": TreeKey,
                        "locationX": locationX,
                        "locationY": locationY,
                        "isNear": true
                    }
                    objects.push(snap)
                } else {
                    //一定距離の中に入っていない自分の作成したTreePot
                    const snap = {
                        "TreeKey": TreeKey,
                        "locationX": locationX,
                        "locationY": locationY,
                        "isNear": false
                    }
                    objects.push(snap)
                }
            })

            const json = JSON.stringify(objects)
            res.send(json)
        } catch (error) { res.end("error") }
    })

    // TreePotを作成
    .post((req, res) => {
        /*
        REQ JSON
        {
        locationX: "XXXX"
        locationY: "XXXX"
        TreeKey: "XXXX"
        }
        */
        const reqlocationX = req.body.locationX
        const reqlocationY = req.body.locationY
        const TreeKey = req.body.TreeKey
        const ref = db.ref('/TreePot')
        try {
            ref.child(TreeKey).set({
                locationX: reqlocationX,
                locationY: reqlocationY,
                TreeKey: TreeKey
            })
            res.send("success")
        } catch (error) {
            res.send("error")
        }

    })

    // TreePotのSessionを切る
    .delete((req, res) => {
        /*
        REQ JSON
        {
        TreeKey: "XXXX"
        }
        */

        const TreeKey = req.body.TreeKey
        const ref = db.ref('/TreePot')
        try {
            //TreePotのデータを削除
            ref.child(TreeKey).remove()
            res.send("success")
        } catch (error) {
            res.send("error")
        }
    })

router.route('/view')
    // 一定距離内のTreePotを返す
    .get((req, res) => {
        /*
        REQ JSON
        {
        locationX: "XXXX"
        locationY: "XXXX"
        distance: "XXXX"
        }
        */
        const ref = db.ref("/TreePot")
        try {

            const reqlocationX = req.body.locationX
            const reqlocationY = req.body.locationY
            const reqdistance = req.body.distance //一定距離

            // 一定距離内のTreePot情報を格納する
            ref.on('child_added', (snapshot) => {
                const locationX = snapshot.val().locationX
                const locationY = snapshot.val().locationY
                const TreeKey = snapshot.val().TreeKey
                const snap = {
                    "locationX": locationX,
                    "locationY": locationY,
                    "TreeKey": TreeKey,
                }
                if (distancejs.distance(reqlocationX, reqlocationY, locationX, locationY, reqdistance)) objects.push(snap)
            })
            const json = JSON.stringify(objects)
            res.send(json)
        } catch (error) { res.end("error") }
    })

module.exports = router