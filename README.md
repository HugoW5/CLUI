

<h3>Label & Dropdown</h3>

```
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

<h3>Label & Checkbox</h3>
	
```
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
</details>

