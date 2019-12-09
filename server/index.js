// Import firebase-functions
const functions = require('firebase-functions')
// Import firebase-admin
const admin = require("firebase-admin")
// Import express
const express = require('express')
// Import Tree.js as Router
const TreeRouter = require("./router/Tree")

// Init express
const app = express()

app.use(bodyParser.urlencoded({ extended: true }))
app.use(bodyParser.json())

const serviceAccount = require("./secrets/p2hack2019-4f273-firebase-adminsdk-jc5yu-ad54cd83f6.json")
// Initialize the app with a service account, granting admin privileges
global.admin.initializeApp({
    credential: admin.credential.cert(serviceAccount),
    databaseURL: "https://p2hack2019-4f273.firebaseio.com",
    storageBucket: "gs://p2hack2019-4f273.appspot.com",
})

// globalオブジェクトに設定 外部ファイルから実行するため
global.db = admin.database()// RealTimeDatabaseのオブジェクト
global.bucket = admin.storage().bucket()// CloudStorageのオブジェクト

//////////////////////////////////////////////
app.use('/Tree', TreeRouter)


//////////////////////////////////////////////

// exportsしてfirebase側から呼び出してもらう
exports.app = functions.https.onRequest(app)

/*
Data構造
Root - User - ${uid} - name : string
                     - uid : ${uid}
                     - pid : ${pid}


     - Tree - ${TreeKey} - locationX : num
                         - locationY : num
                         - owner : ${pid}
                         - point : num
                         - TreeKey : ${TreeKey}

     - TreeDecoration - ${TreeKey} - Decoration(max <= 100) - ${DecorationKey} - kind : num
                                                                               - posX : num
                                                                               - posY : num
                                                                               - timestamp : num
                                                                               - message: string

     - TreePot - ${TreePotKey} - locationX : num //Session一時的
                               - locationY : num
                               - owner: ${TreeKey}

     - Account - ${pid} - locationX : num
                        - locationY : num
                        - name : string
                        - profile : string
                        - point : num

     - Event - &{EventKey} - due : num
                           - owner : ${pid}
                           - message : string
                           - kind : string
*/