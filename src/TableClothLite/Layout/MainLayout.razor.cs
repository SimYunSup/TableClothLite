using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using TableClothLite.Components.Chat;
using TableClothLite.Components.Setting;

namespace TableClothLite.Layout;

public partial class MainLayout : LayoutComponentBase
{
    public async Task OpenChatPage()
    {
        var apiKey = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "openRouterApiKey");

        if (string.IsNullOrEmpty(apiKey))
        {
            // API Ű�� ���� ��� �ȳ� ��ȭ ���� ǥ��
            var result = await DialogService.ShowDialogAsync<OpenRouterGuide>(
                new DialogParameters()
                {
                    Title = "OpenRouter ���� �ʿ�",
                    PreventScroll = true,
                    PrimaryAction = "����ϱ�",
                    SecondaryAction = "���",
                    Width = "450px"
                });

            // ����ڰ� ����ϱ⸦ ������ ��쿡�� ���� �÷ο� ����
            if (await result.GetReturnValueAsync<bool>() == true)
                await AuthService.StartAuthFlowAsync();
        }
        else
        {
            NavigationManager.NavigateTo("/Chat");
        }
    }

    public async Task OpenSettingDialog()
    {
        await DialogService.ShowDialogAsync<Setting>(
            new DialogParameters()
            {
                PreventScroll = true
            }
        );
    }
}
