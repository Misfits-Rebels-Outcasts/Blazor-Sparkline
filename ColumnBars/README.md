# Blazor Sparkline Column Bars using Fonts

To use the Column Bars

<p>
<img width="122" height="109"  src=https://webassemblyman.com/blazor/images/blazorcolumnbars.png />
</p>   

1. Create your Blazor project.

2. In YourProject.csproj, add

&lt;ProjectReference Include="../Blazor-Sparkline/ColumnBars/ColumnBars.csproj" />

3. In index.html, add

&lt;link href="_content/ColumnBars/styles.css" rel="stylesheet" />

4. Finally, in Pages->Index.razor, add

&lt;ColumnBarsMan <br /> 
InputData="[21,30,111,114,140,158,206,249,262,266,285,340,428,81,206,249,262,440,158,206,249,262,266,285,340,428], <br />[27,29,95,216,228,242,287,362,369,372,380,433,479,206,249,262,114,540,558,206,249,362,266,285,340,428], <br />[21,30,111,114,140,158,206,249,262,266,285,340,428,81,206,249,262,440,158,206,249,262,266,285,340,428], <br />[27,29,95,216,228,242,287,362,369,372,380,433,479,206,249,262,114,540,558,206,249,362,266,285,340,428]">
&lt;/ColumnBarsMan>
