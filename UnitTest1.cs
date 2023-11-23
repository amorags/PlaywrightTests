using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    //Run with debugger: PWDEBUG=1 dotnet test
    
    [Test]
    public async Task CanFieldBeFound()
    {
        await Page.GotoAsync("https://demo.playwright.dev/todomvc/");
        var inputField = Page.GetByPlaceholder("What needs to be done?");
        await Expect(inputField).ToBeVisibleAsync();
        await Expect(inputField).ToBeEditableAsync();
        await Expect(inputField).ToBeEmptyAsync();
    }
    
    ///here is commentary

    [Test]
    public async Task LocateElements()
    {
        await Page.GotoAsync("https://demo.playwright.dev/todomvc/");
        var todosBanner = Page.GetByRole(AriaRole.Heading, new() { Name = "todos" });
        var headerMessage = Page.GetByText("This is just a demo of TodoMVC for testing, not the real TodoMVC app.");
        var linkToTodoMvc = Page.GetByRole(AriaRole.Link, new() { Name = "TodoMVC", Exact = true });
        await Expect(todosBanner).ToBeVisibleAsync();
        await Expect(headerMessage).ToBeVisibleAsync();
        await Expect(linkToTodoMvc).ToBeVisibleAsync();
    }

    [Test]
    public async Task TestWithAction()
    {
        await Page.GotoAsync("https://demo.playwright.dev/todomvc/");
        await Page.GetByPlaceholder("What needs to be done?").ClickAsync();
        await Page.GetByPlaceholder("What needs to be done?").FillAsync("Item text");
        await Page.GetByPlaceholder("What needs to be done?").PressAsync("Enter");
        await Expect(Page.Locator("li").Filter(new() { HasText = "Item text" })).ToBeVisibleAsync();
    }

    [TestCase("Todo item 1 string here")]
    [TestCase("Todo item 2 string here")]
    public async Task InlinedTest(string todoString)
    {
        await Page.SetViewportSizeAsync(300, 300);
        await Page.GotoAsync("https://demo.playwright.dev/todomvc/");
        await Page.GetByPlaceholder("What needs to be done?").ClickAsync();
        await Page.GetByPlaceholder("What needs to be done?").FillAsync(todoString);
        await Page.GetByPlaceholder("What needs to be done?").PressAsync("Enter");
        await Expect(Page.Locator("li").Filter(new() { HasText = todoString })).ToBeVisibleAsync();
    }
    
    [TestCase("Todo item 1 string here", 300, 300)]
    [TestCase("Todo item 2 string here", 400, 600)]
    public async Task InlinedTestWithBothDataAndViewport(string todoString, int width, int height)
    {
        await Page.SetViewportSizeAsync(width, height);
        await Page.GotoAsync("https://demo.playwright.dev/todomvc/");
        await Page.GetByPlaceholder("What needs to be done?").ClickAsync();
        await Page.GetByPlaceholder("What needs to be done?").FillAsync(todoString);
        await Page.GetByPlaceholder("What needs to be done?").PressAsync("Enter");
        await Expect(Page.Locator("li").Filter(new() { HasText = todoString })).ToBeVisibleAsync();
    }
}