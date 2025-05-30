# CLUI ![NuGet Version](https://img.shields.io/nuget/v/CLUI) ![GitHub branch check runs](https://img.shields.io/github/check-runs/HugoW5/CLUI/master)

## Nuget
https://www.nuget.org/packages/CLUI
## Example app
![clui](https://github.com/user-attachments/assets/b6d332a5-23ab-447b-9bd1-ec73282600a2)

## Components
1. Button
2. Label
3. Rect
4. TextBox
5. PasswordBox
6. Checkbox
7. Dropdown
8. GridPanel
9. StackPanel

## Label
```cs
Window window = new Window(0, 0, 30, 12);
window.AddComponent(new Label
{
	X = 15,
	Y = 3,
	Width = 20,
	HorizontalAlignment = HorizontalAlignment.Center,
	Text = "Label1",
});
window.Render();
window.HandleInput();
```

## Label & Dropdown

```cs
Window window = new Window(0, 0, 30, 12);
window.AddComponent(new Label
{
	X = 15,
	Y = 3,
	Width = 20,
	HorizontalAlignment = HorizontalAlignment.Center,
	Text = "Label1",
});
window.AddComponent(new Dropdown
{
	X = 0,
	Y = 0,
	Options = {
		"Volvo",
		"Saab",
		"BMW",
		"Opel",
		"Skóda"
	},
	OnSelected = (int index) =>
	{
		var label = ((Label)window.components[0]);
		label.Text = index.ToString();
		label.Render(window.X, window.Y);
	}
});
window.Render();
window.HandleInput();
```

## Label & Checkbox
	
```cs
Window window = new Window(0, 0, 30, 12);
window.AddComponent(new Label
{
	Width = 25,
	HorizontalAlignment = HorizontalAlignment.Center,
	X = 5,
	Y = 0,
	Text = "Label 1",
});
window.AddComponent(new Checkbox
{
	X = 0,
	Y = 0,
	Checked = true,
	OnClicked = (bool _checked) =>
	{
		string text = (_checked ? "Check box is cheked" : "Check box is not cheked");
		var label = ((Label)window.components[0]);
		label.Text = text;
		label.Update();
	}
});
```
