using Fluent;

/// <summary>
/// Multilingual 
/// </summary>
public class Conversation13 : MyFluentDialogue
{
    private void SetLanguage(Language language)
    {
        LanguageManager.CurrentLanguage = language;
    }

    public override void OnFinish()
    {
        LanguageManager.CurrentLanguage = Language.English;
        base.OnFinish();
    }

    public override FluentNode Create()
    {
        return
            Show() *
            Write(0, speakManyLanguages) *
            Options
            (
                Option().Text(switchToEnglish).VisibleIf(() => LanguageManager.CurrentLanguage != Language.English) *
                    Do(() => SetLanguage(Language.English)) *

                Option().Text(switchToAfrikaans).VisibleIf(() => LanguageManager.CurrentLanguage != Language.Afrikaans) *
                    Do(() => SetLanguage(Language.Afrikaans)) *

                Option().Text(singMeASong) *
                    Write(singSong) *

                Option().Text(bye) *
                    Hide() *
                    Yell(bye) *
                    End()
            );
    }

    object[] speakManyLanguages = {
                Language.English, "I speak a couple of languages",
                Language.Afrikaans, "Ek praat 'n paar tale" };
    object[] singSong = {
                        Language.English, englishSong,
                        Language.Afrikaans, afrikaansSong };
    object[] switchToEnglish = {
                Language.Afrikaans, "Skuif na Engels (*)"};
    object[] switchToChinese = {
                Language.Afrikaans, "Skuif na Shinees"};
    object[] switchToAfrikaans = {
                Language.English, "Switch to Afrikaans" };
    object[] singMeASong = {
                Language.English, "Sing me a song!",
                Language.Afrikaans, "Sing vir my 'n liedjie!" };
    object[] bye = {
                Language.English, "Bye",
                Language.Afrikaans, "Totsiens" };


    const string afrikaansSong =
@"Wielie, Wielie, Waalie!
Die aap ry op sy baalie!
Tjoef tjaf val hy af
Wielie, Wielie, Waalie!";

    const string englishSong =
@"Ring-a-round the rosie,
A pocket full of posies,
Ashes! Ashes!
We all fall down!";

}
