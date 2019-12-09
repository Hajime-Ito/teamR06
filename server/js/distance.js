/*
b1018194 Ito Hajime
distance.js
一定の距離以内ならtrueを返す
*/
exports.distance = (x, y, x1, y1, max_distance) => {
    let distance = Math.sqrt((x - x1) ^ 2 + (y - y1) ^ 2)
    if (max_distance >= distance) return true
    else return false
}