/*
b1018194 Ito Hajime
Hotspot.js
Hotspotを計算・配信する
*/

// Import express
const express = require('express')
// Import bodyParser
const bodyParser = require('body-parser')

const app = express()

app.use(bodyParser.urlencoded({ extended: true }))
app.use(bodyParser.json())

const router = express.Router()

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

    // hotspotを計算して返す
    .get((req, res) => {
        /*
        REQ JSON
        {
        locationX: "XXXX"
        locationY: "XXXX"
        distance: "XXXX"
        }
        */
        
    })

module.exports = router