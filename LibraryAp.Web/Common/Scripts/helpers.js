var App = App || {};
(function () {

    var appLocalizationSource = abp.localization.getSource('LibraryAp');
    App.localize = function () {
        return appLocalizationSource.apply(this, arguments);
    };

})(App);