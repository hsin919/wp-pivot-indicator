#wp-pivot-indicator
==================

An iOS pagecontrol like object based on [Jeremy's](https://github.com/japf/wp-pivot-indicator) excellent works.
Here is the snapshot of my works.

![Screenshot](https://www.evernote.com/shard/s90/sh/bb1f52b5-b532-43f2-a93f-992ed3e5abed/ced52ea1aa3cae9e3f09f4b8b3b61cf2/res/51d55271-684a-4d1a-91ed-69c74a390620/wp_ss_20140324_0001.png?resizeSmall&width=832)

## Example Usage
Just add these code after your pivot control.
``` c#
<control:PivotIndicator
        Grid.Row="2"
        indicateWidth="11"
        Height="30"
        Width="200"
        Pivot="{Binding ElementName=pivot}">
    <control:PivotIndicator.IndicatorForeground>
        <SolidColorBrush Color="#1E1E1E"/>
    </control:PivotIndicator.IndicatorForeground>
    <control:PivotIndicator.IndicatorBackgroundFill>
        <SolidColorBrush Color="#FFFFFF"/>
    </control:PivotIndicator.IndicatorBackgroundFill>
    <control:PivotIndicator.IndicatorBackgroundStroke>
        <SolidColorBrush Color="LightGray"/>
    </control:PivotIndicator.IndicatorBackgroundStroke>
    <control:PivotIndicator.HeaderTemplate>
        <DataTemplate>
            <!-- this the datatemplate used for each item in the PivotIndicator
                 the DataContext is a PivotItemViewModel in our case -->
            <TextBlock 
                Text=""
                HorizontalAlignment="Center"/>
        </DataTemplate>
    </control:PivotIndicator.HeaderTemplate>
</control:PivotIndicator>
```
##  Customization Details

`Height` & `Width` are the size of PivotIndicator panel and `indicateWidth` stands for the dot size.
User can customize the color of the indicator by using `IndicatorForeground`, `IndicatorBackgroundFill`, and `IndicatorBackgroundStroke`.


