# Bullet Rain version 0.0.6

## version 0.0.5からの変更 *内部的変更のみ*
+ グローバルレベルのバランス調整
	* よりレベルが上がりやすく。

+ 内部アルゴリズムを一部変更
	* より軽量に。

+ ガベージコレクタの呼び出しタイミングを変更
	* ゾンビ化してメモリリークしていたオブジェクトが破棄されるように。

- 既存のバグを修正

## 将来的なアップデート
+ 敵の種類を追加
+ フェーズごとの敵の強さの変更
+ ボスの種類を追加
+ ショップ、強化を追加
- その他バグフィクス

## 操作
|キー|動作|
|:-:|:-:|
|W|上方向移動|
|S|下方向移動|
|A|左方向移動|
|D|右方向移動|
|Space|弾を発射|

## 諸注意事項
1.起動時、Screen resolutionを1366x768にすること。* それ以外の場合、一部レイアウトが崩れる恐れあり。

2.起動時、Graphics qualityをGood以下にすること。* 描画クオリティを上げると非常に重くなる可能性があります。

## ライセンス
このソフトウェアは、以下のライセンスの元利用できます。

BSD 2-clause "Simplified" License

以下、ライセンス条項
```
BSD 2-Clause License

Copyright (c) 2017, frodo821
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this
  list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice,
  this list of conditions and the following disclaimer in the documentation
  and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
```
