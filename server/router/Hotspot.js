/*
b1018194 Ito Hajime
Hotspot.js
Hotspotを計算・配信する
*/

// Import express
const express = require('express')
// Import bodyParser
const bodyParser = require('body-parser')
// Import hotspot.js
const hotspot = require("../js/hotspot")

const app = express()

app.use(bodyParser.urlencoded({ extended: true }))
app.use(bodyParser.json())

const router = express.Router()

let locations = []

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
        const locationX = req.query.locationX
        const locationY = req.query.locationY
        const distance = req.query.distance
        const ref = db.ref("/Account")
        ref.on('child_added', (snapshot) => {
            const location = {
                x: snapshot.val().locationX,
                y: snapshot.val().locationY
            }
            locations.push(location)
        })
        const obj = hotspot.gethotspot(locationX, locationY, distance, locations)
        const json = JSON.stringify(obj)
        res.send(json)
        locations = []
    })

module.exports = router