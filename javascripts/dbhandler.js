function addSession(time, tries, isColorSynced){

    if (isColorSynced) {
        var syncedRef = new Firebase("https://blazing-inferno-8421.firebaseio.com/sessions/colorSyncedSessions");
        syncedRef.push({ "time": time, "tries": tries });

    }
    else {
        var nonSyncedRef = new Firebase("https://blazing-inferno-8421.firebaseio.com/sessions/NonColorSyncedSessions");
        nonSyncedRef.push({ "time": time, "tries": tries });
    }	

}