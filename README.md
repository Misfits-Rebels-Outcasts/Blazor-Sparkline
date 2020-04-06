# Blazor-Sparkline
Sparkline Charts for Blazor using Fonts

Open Source Sparkline Charts for Blazor and WebAssembly. It uses C#, HTML and CSS with minimal Javascript dependencies. 

<p>
<img width="358" height="550"  src=https://webassemblyman.com/blazor/images/blazorsparklines.png />
</p>   

Sparklines are typically used to display summary information and they commonly appear in multiple rows of a HTML table. 
This component uses Open Type Fonts and does not require a SVG or Canvas element. The font is downloaded once and then used to display the Sparkline charts multiple times.

The current font uses dots to draw each segment of the Sparkline and is useful when you have many data points. The width of each segment can be specified by using the SegmentWidth parameter. A bigger SegmentWidth results in a longer Sparkline. It also means more data characters (dots) need to be generated to display the Sparkline using the font. Additional fonts will be added to support specific SegmentWidth instead of just a dot. This enables the the reduction of data characters generated.

&num;elegantlysimple

v0.2

[![MIT Licence](https://www.webassemblyman.com/images/mitlicense.png)](https://www.webassemblyman.com/MITLicense.txt)

### To use Sparkline

1. Create your Blazor project.

2. In YourProject.csproj, add

&lt;ProjectReference Include="../Blazor-Sparkline/Sparklines/Sparklines.csproj" />

3. In index.html, add

&lt;link href="_content/Sparklines/styles.css" rel="stylesheet" />

4. Finally, in Pages->Index.razor, add

&lt;Sparkline InputData="60,0,16,25,48,45,1,0,6,37,78,79,90,90,91,99,80,0,40,0,0,56" GenerateText="true" SegmentWidth="30">&lt;/Sparkline>

### To use Column Bars

1. Create your Blazor project.

2. In YourProject.csproj, add

&lt;ProjectReference Include="../Blazor-Sparkline/ColumnBars/ColumnBars.csproj" />

3. In index.html, add

&lt;link href="_content/ColumnBars/styles.css" rel="stylesheet" />

4. Finally, in Pages->Index.razor, add

&lt;ColumnBars InputData="221,330,111,114,140,158,206,249,262,266,285,340,428,81,206,249,262,440,158,206,249,262,266,285,340,428" GenerateText="true" >&lt;/ColumnBars>

Similary

### To use Bullet Chart

&lt;ProjectReference Include="../Blazor-Sparkline/BulletBars/BulletBars.csproj" />

&lt;link href="_content/BulletBars/styles.css" rel="stylesheet" />

&lt;BulletBars InputData="26,40,95,100" Actual="49" Target="53"  GenerateText="true"><br />&lt;/BulletBars>

### To use MiniPie

&lt;ProjectReference Include="../Blazor-Sparkline/MiniPie/MiniPie.csproj" />

&lt;link href="_content/MiniPie/styles.css" rel="stylesheet" />

&lt;MiniPie InputData="26,40,95,100" GenerateText="true"><br />&lt;/MiniPie>

### To use Win Loss Draw

&lt;ProjectReference Include="../Blazor-Sparkline/WinLoss/WinLoss.csproj" />

&lt;link href="_content/WinLoss/styles.css" rel="stylesheet" />

&lt;WinLoss InputData="1,1,1,-1,1,1,1,0,1,-1,1,1,1,1,1,0,1,-1,1,1,1,1,0,0,0,1,1,1,0,1,1,-1,1,1,1,1" GenerateText="true"><br />&lt;/WinLoss>

Alternatively, check out our [Misfits-Rebels-Outcasts/Blazor-Dashboard](https://github.com/Misfits-Rebels-Outcasts/Blazor-Dashboard) project on how to use this Blazor Sparkline component.
