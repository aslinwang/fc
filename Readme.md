#fc-a Fiddler plugin to display remote log#

* 在移动开发过程中，我们经常使用Fiddler做代理，利用autoResponsder功能，将一些js文件映射到PC本地，在PC上进行开发。
* 由于移动浏览器的特殊性，无法方便的查看在js代码中利用console.log打印的一些调试信息
* fc,FiddlerConsole,是一个Fiddler插件，可以显示远程的log信息。

##用法##
* 下载[fiddlerconsolesetup.exe](https://github.com/aslinwang/fc/blob/master/fiddlerconsolesetup.exe)，在PC上进行安装
* 在js代码中引入fc.js文件（支持AMD的方式,可使用此地址：http://mat1.gtimg.com/www/mb/js/lib/fc.js ）
* 在js中调用fc.log进行打印，如：

    ```javascript
    /**
     * 默认调用——
     * 参数1~n - 要打印的值（字符串或对象）
     */
    fc.log({a:1,b:2}, 'something string want to display');
    
    /**
     * 带标识调用——
     * 参数1~n-1 - 要打印的值（字符串或对象）
     * 参数n - {fcflag:'print 1'} "print 1"为fc标识（字符串，可任意填写，便于在fiddler中找到该次打印对应的session） 
     */
    fc.log('something want to display',{name:'aslin', age:'23', job:'code farmer'}, {fcflag:'print 1'});
    
    fc.enable = false//禁用此功能
    ```
 
* 在fiddler中查看黑色背景、白色前景的session，双击查看返回内容即为打印的结果![session](http://t2.qpic.cn/mblogpic/cf0ed62451b7fb150ede/2000)![response](http://t2.qpic.cn/mblogpic/6c5ac420425012d741d4/2000)

##change log##
30 May 2014
* 解决ajax请求fiddler.fc.com时的跨域报错
* 优化fc.log的调用方式，弱化fcflag参数