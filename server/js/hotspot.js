/*
b1018194 Ito Hajime
HotSpot.js
hotspotを求める
*/
let mylocate = { x: 0, y: 0 }
let locateobj = [{ x: 1, y: 0 }, { x: 1, y: 1.5 }, { x: 2, y: 2 },
{ x: 3, y: 1 }, { x: 1, y: 3 }, { x: 3, y: 3 },
{ x: 3, y: 2.5 }, { x: 2, y: 3 }, { x: 5, y: 5 }, { x: 1, y: 1.7 }]

const distancefromMe = 10
const distance_min = 1.2
let obj = []

for (let i = 0; i < locateobj.length; i++) {
    let x1 = locateobj[i].x
    let y1 = locateobj[i].y
    if (Math.sqrt((x1 - mylocate.x) ^ 2 + (y1 - mylocate.y) ^ 2) > distancefromMe) {
        let idx = locate.indexOf({ x: x1, y: y1 })
        locate.splice(idx, 1)
        break
    }
    for (let j = 0; j < locateobj.length; j++) {
        let x2 = locateobj[j].x
        let y2 = locateobj[j].y
        if (i == j) break
        if (Math.abs((Math.sqrt((x1 - mylocate.x) * (x1 - mylocate.x) + (y1 - mylocate.y) * (y1 - mylocate.y))) - (Math.sqrt((x2 - mylocate.x) * (x2 - mylocate.x) + (y2 - mylocate.y) * (y2 - mylocate.y)))) < distance_min) {
            if (Math.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)) < distance_min) {
                //console.log(Math.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)))
                obj.push({ x1: x1, y1: y1, x2: x2, y2: y2, num: i })
            }
        }
    }
}

let tmpcount = 0
let tmpx = 0
let tmpy = 0
let count = 1
let tmpArray = []

for (let i = 0; i < locateobj.length; i++) {
    let bool = false
    for (let j = 0; j < obj.length; j++) {
        if (i == obj[j].num) {
            count++
            bool = true
            tmpx += obj[j].x2
            tmpy += obj[j].y2
        }
    }
    if (bool) {
        tmpx += locateobj[i].x
        tmpy += locateobj[i].y
        tmpArray.push({ hotx: tmpx / count, hoty: tmpy / count, count: count })
        count = 1
        bool = false
    }
}

console.log(obj)
console.log(tmpArray)