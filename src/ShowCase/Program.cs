using CLUI;
using CLUI.Components;
using CLUI.Enums;
using CLUI.Layouts;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Principal;


namespace ShowCase
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.CursorVisible = false;
			Window window = new Window(0, 0,40, 20);

			int count = 1;
			window.AddComponent(new Label
			{
				X = 0,
				Y = 2,
				Width = window.Width,
				HorizontalAlignment = HorizontalAlignment.Center,
				Text = count.ToString(),
				BackGroundColor = ConsoleColor.White,
				Id = "count"
			});

			window.AddComponent(new StackPanel
			{
				X = 0,
				Y = 4,
				Width = window.Width,
				BackGroundColor = ConsoleColor.White,
				StackingAlignment = StackingAlignment.Horizontal,
				Spacing = 2,
				Children = {
					new Button{
						Text="Add (+)",
						Width = (window.Width/2)-1,
						HorizontalAlignment= HorizontalAlignment.Center,
						BackGroundColor= ConsoleColor.DarkGreen,
						FocusColors=(ConsoleColor.Green,
						ConsoleColor.White),
					Click= () => {
						count++;
						((Label)window.GetComponentById("count")).Text = "Amount: " + count.ToString();
						((Label)window.GetComponentById("count")).Update();
					}
					},
					new Button{
						Text="Subtract (-)",
								Width = (window.Width/2)-1,
						HorizontalAlignment= HorizontalAlignment.Center,
						BackGroundColor= ConsoleColor.DarkRed,
						FocusColors=(ConsoleColor.Red,
						ConsoleColor.White),
						Click= () => {
						count--;
						((Label)window.GetComponentById("count")).Text = "Amount: " + count.ToString();
						((Label)window.GetComponentById("count")).Update();
					}
					},
			}
			});

			window.AddComponent(new Button
			{
				X = 5,
				Y = 6,
				Width = window.Width-10,
				Text = "Reset Counter",
				HorizontalAlignment = HorizontalAlignment.Center,
				Click = () =>
				{
					count = 0;
					((Label)window.GetComponentById("count")).Text = "Amount: " + count.ToString();
					((Label)window.GetComponentById("count")).Update();
				}
			});


			window.AddComponent(new Dropdown
			{
				X = 12,
				Y = 8,
				Options = {
					"+10",
					"+100",
					"+1000",
					"+1000000",
					"+1000000000",
				},
				OnSelected = (int index) =>
				{
					int plusCount = 0;
					switch (index)
					{
						case 0:
							plusCount = 10;
							break;
						case 1:
							plusCount = 100;
							break;
						case 2:
							plusCount = 1000;
							break;
						case 3:
							plusCount = 1000000;
							break;
						case 4:
							plusCount = 1000000000;
							break;
					}

					count += plusCount;
					((Label)window.GetComponentById("count")).Text = "Amount: " + count.ToString();
					((Label)window.GetComponentById("count")).Update();
				}

			});

			window.Render();
			window.HandleInput();
		}
	}
}
