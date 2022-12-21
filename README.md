## Description

On Android only the first tab within Shell calls the Page's OnAppearing-method,
causing a variety of issues like not triggering initialization code or initializing
view components (InvalidateMeasure will have do be called to force a view to
relayout after more items were added).

## Github issues

* dotnet/maui#8102
* dotnet/maui#8164
* dotnet/maui#9531
* xamarin/Xamarin.Forms#3855

## Steps to Reproduce

Run the attached sample on Android
* Hit the _Other_ tab and then the _Other 2_ tab
  * Page shows that OnAppearing was not called
* Hit the _Refresh_ button
  * Items are added to ObservableCollection but not visible on the page
* Hit the _InvalidateMeasure_ button
  * Now Items are visible on the page
* Hit the _Add result_ button
  * Results are added to the ObservableCollection and rendered on the page

## Version with bug
7.0.100 (current)

## Last version that worked well
Unknown

## Affected platforms
Android

## Affected platform versions
Android 13 (and probably all earlier versions)

## Did you find any workaround?
No workaround for the OnAppearing-issue.<br>
Calling `InvalidateMeasure` seems to fix the layout issues. 

## Relevant log output
No response