# WPF贝塞尔曲线示例

贝塞尔曲线在之前使用SVG的时候其实就已经有接触到了，但应用未深，了解的不是很多，最近在进行图形操作的时候需要用到贝塞尔曲线，所以又重新来了解WPF中贝塞尔曲线的绘制。

1. 一阶贝塞尔曲线
一阶贝塞尔实际上就是一条直线，它的公式为：

![一阶贝塞尔曲线公式](https://github.com/ZZRRegion/BezierDemo/blob/master/%E5%9B%BE%E7%89%87/%E4%B8%80%E9%98%B6%E8%B4%9D%E5%A1%9E%E5%B0%94%E6%9B%B2%E7%BA%BF%E5%85%AC%E5%BC%8F.png?raw=true)  

示例动图：
![一阶贝塞尔曲线动图](https://raw.githubusercontent.com/ZZRRegion/BezierDemo/master/%E5%9B%BE%E7%89%87/%E4%B8%80%E9%98%B6%E8%B4%9D%E5%A1%9E%E5%B0%94%E6%9B%B2%E7%BA%BF.gif)

2. 二阶贝塞尔曲线
假设现在有A,B,C三点，需要绘制贝塞尔曲线，比例t(0~1)。
计算过程：
在AB上取t比例点a,在BC上取t比例点b.连接ab两点，再在ab上取t比例点c。则c就是贝塞尔曲线上的点了。
计算公式如下:

![二阶贝塞尔曲线公式](https://raw.githubusercontent.com/ZZRRegion/BezierDemo/master/%E5%9B%BE%E7%89%87/%E4%BA%8C%E9%98%B6%E8%B4%9D%E5%A1%9E%E5%B0%94%E6%9B%B2%E7%BA%BF%E5%85%AC%E5%BC%8F.png)  

示例动图：
![二阶贝塞尔曲线动图](https://raw.githubusercontent.com/ZZRRegion/BezierDemo/master/%E5%9B%BE%E7%89%87/%E4%BA%8C%E9%98%B6%E8%B4%9D%E5%A1%9E%E5%B0%94%E6%9B%B2%E7%BA%BF.gif)

3. 三阶贝塞尔曲线
三阶比二阶多计算一条，计算过程与二阶是类似的。
计算公式如下：  

![三阶贝塞尔曲线公式](https://raw.githubusercontent.com/ZZRRegion/BezierDemo/master/%E5%9B%BE%E7%89%87/%E4%B8%89%E9%98%B6%E8%B4%9D%E5%A1%9E%E5%B0%94%E6%9B%B2%E7%BA%BF%E5%85%AC%E5%BC%8F.png)  
示例动图：  
![三阶贝塞尔曲线动图](https://raw.githubusercontent.com/ZZRRegion/BezierDemo/master/%E5%9B%BE%E7%89%87/%E4%B8%89%E9%98%B6%E8%B4%9D%E5%A1%9E%E5%B0%94%E6%9B%B2%E7%BA%BF.gif)
这是[示例程序](https://github.com/ZZRRegion/BezierDemo)，有需要的可以下载查看，里面有比较详细的计算过程