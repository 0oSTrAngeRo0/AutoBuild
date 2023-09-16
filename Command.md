## 命令行打包工具

### 准备工作

将BuildPlayer放到项目的Assets文件夹下

### 参数详解

-scenes \<scenes1 scenes2\> 设置需要打包的场景

-targetPath \<path\> 设置打包位置

### 示例

"C:\Program Files\Unity\Hub\Editor\2023.1.8f1\Editor\Unity.exe" -quit -batchmode -projectPath "D:\Unity\WorkSpace\SRTK\SALSA" -executeMethod BuildUtility.BuildPlayer.Build -scenes "Assets/Scenes/Portrait2DScene.unity" -targetPath "D:\Unity\WorkSpace\SRTK\SALSA\Build\build.exe"

"C:\Program Files\Unity\Hub\Editor\2023.1.8f1\Editor\Unity.exe" 设置UnityEditor的路径

-quit 表示打包完成后自动退出

-batchmode 表示静默模式打包

-projectPath "D:\Unity\WorkSpace\SRTK\SALSA" 设置项目路径

-executeMethod BuildUtility.BuildPlayer.Build 设置打包执行函数

### 参考文献

UnityEditor命令行参数文档: https://docs.unity3d.com/Manual/EditorCommandLineArguments.html

