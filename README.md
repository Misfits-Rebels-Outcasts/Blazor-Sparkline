# Blazor-Sparkline
Sparkline Charts for Blazor using Fonts

Open Source Sparkline Charts for Blazor and WebAssembly. It uses C#, HTML and CSS with minimal Javascript dependencies. 

<p>
<img width="206" height="106"  src=https://webassemblyman.com/blazor/images/blazorsparkline.png />
</p>   

Sparklines are typically used to display summary information and they commonly appear in multiple of a HTML table. 
This component uses Open Type Fonts and does not require a SVG or Canvas element. The font is downloaded once and then used to display the Sparkline charts multiple times.

The current font uses dots to draw each segment of the Sparkline and is useful when you have many data points. The width of each segment can be specified by using the SegmentWidth parameter. A bigger SegmentWidth results in a longer Sparkline. It also means more data characters (dots) need to be generated to display the Sparkline using the font. Additional fonts will be added to support specific SegmentWidth instead of just a dot. This enables the the reduction of data characters generated.

&num;elegantlysimple

v0.1

[![MIT Licence](https://www.webassemblyman.com/images/mitlicense.png)](https://www.webassemblyman.com/MITLicense.txt)

To use the Sparkline

1. Create your Blazor project.

2. In YourProject.csproj, add

&lt;ProjectReference Include="../Blazor-Sparkline/Blazor-Sparkline.csproj" />

3. In index.html, add

&lt;link href="_content/Blazor-Sparkline/styles.css" rel="stylesheet" />

4. Finally, in Pages->Index.razor, add

&lt;SparklineMan <br />
  InputData="[30,0,6,20,45,48,51,60,76,77,78,79,90,100,30,90,95,100,40,0,0,0],<br />[80,0,16,25,48,45,1,0,6,37,78,79,90,90,91,95,80,0,40,0,0,0]" <br />StartColor="#ce4b99" StopColor="#377bbc" SegmentWidth="30"><br />
&lt;/SparklineMan>

Alternatively, check out our Elegantly Simple [Misfits-Rebels-Outcasts/Blazor-Dashboard](https://github.com/Misfits-Rebels-Outcasts/Blazor-Dashboard) project on how to use this Blazor Sparkline component.
