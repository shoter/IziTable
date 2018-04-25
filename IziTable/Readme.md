## IziTable
### License: MIT

### Description

Library for quickly creating HTML table that is easy to insert everywhere (for example inside email) with option to style it with inline CSS.


#### Example usage


```csharp
var table = new Table()
                .AddHeader("Name")
                .AddHeader("Grade")

                .AddRow("John", 5)
                .AddRow("Johny", 2)
                .AddRow("Johnathan", 1)

                .SetStyle(new FileCssStyle("style.css"));

string output = table.ToString();
```

##### style.css

```css
table th
{
    color: red;
}
```

Html Preview:

![Html Preview Image](https://github.com/shoter/IziTable/raw/master/Presentation/example.png)