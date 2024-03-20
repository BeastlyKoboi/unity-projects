mergeInto(LibraryManager.library, {
    SaveDataToLocalStorage: function(key, value) {
        localStorage.setItem(key, value);
    },

    LoadDataFromLocalStorage: function(key) {
        return localStorage.getItem(key);
    },

    ClearLocalStorage: function() {
        localStorage.clear();
    }
});