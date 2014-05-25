#fc-a Fiddler plugin to display remote log#

* 在移动开发过程中，我们经常使用Fiddler做代理，利用autoResponsder功能，将一些js文件映射到PC本地，在PC上进行开发。
* 由于移动浏览器的特殊性，无法方便的查看在js代码中利用console.log打印的一些调试信息
* fc,FiddlerConsole,是一个Fiddler插件，可以显示远程的log信息。

##用法##
* 下载fiddlerconsolesetup.exe，在PC上进行安装
* 在js代码中引入fc.js文件（支持AMD的方式）
* 在js中调用fc.log进行打印，如：
	```javascript
	/**
	 * 参数1 - 标识（字符串，可任意填写，便于在fiddler中找到该次打印对应的session） 
 	 * 参数2~n - 要打印的值（字符串或对象）
 	 */
 	fc.log('print 1', 'something want to display');
 	fc.log('print 2', 'something want to display',{name:'aslin', age:'23', job:'code farmer'});
	```