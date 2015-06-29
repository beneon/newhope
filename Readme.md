this is the testing ground for the repo
===

chunk.cs is tested now by world.cs

- Chunk(), SetBlockWorldPos, SetBlock, DelBlockWorldPos, DelBlock, GetBlockWorldPos, GetBlock tested
- WorldToLocateX & Y & Z, X, Y, Z, WorldX & Y & Z tested
- NeighboursReady not done yet

###2015-6-29 15:45
@World.cs

```cs
chunk = new Chunk(this, x, y, z);
```

虽然chunk在这里已经新建了，但是grid还是空的，这里只要呼叫一下grid的descrption就知道了：

```
the grid now is ranged in
X 0,0in Y 0,0in Z 0,0
```

所以这个时候呼叫chunk.NeighboursReady的话肯定会出错的。

@grid.cs

AddOrReplace里面肯定有一定的问题，主要体现在从负数增加到0的时候，会出现数组范围超限。

我觉得应该把min，max这些设置交给constructor来做，**直接从0开始确实难以保证不出问题**。
