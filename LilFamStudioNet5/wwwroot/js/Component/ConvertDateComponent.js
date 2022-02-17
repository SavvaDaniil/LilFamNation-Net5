

class ConvertDateComponent {

    static getDateWithTimeOrNull(dateTimeStr) {
        if (dateTimeStr == null) return null;
        var dateTime = new Date(dateTimeStr); if (dateTime == null) return null;

        var hours = ('0' + dateTime.getHours()).slice(-2);
        var minutes = ('0' + dateTime.getMinutes()).slice(-2);

        var day = ('0' + dateTime.getDate()).slice(-2);
        var month = ('0' + (dateTime.getMonth() + 1)).slice(-2);
        var year = dateTime.getFullYear();

        return hours + ":" + minutes + " " + day + '/' + month + '/' + year;
    }

    static getDateOrNull(dateTimeStr) {
        if (dateTimeStr == null) return null;
        var dateTime = new Date(dateTimeStr); if (dateTime == null) return null;

        var day = ('0' + dateTime.getDate()).slice(-2);
        var month = ('0' + (dateTime.getMonth() + 1)).slice(-2);
        var year = dateTime.getFullYear();

        return day + '/' + month + '/' + year;
    }

    static getDay(dateTimeStr) {
        if (dateTimeStr == null) return null;var dateTime = new Date(dateTimeStr); if (dateTime == null) return null;
        var day = ('0' + dateTime.getDate()).slice(-2);
        return day ;
    }

    static getMonth(dateTimeStr) {
        if (dateTimeStr == null) return null; var dateTime = new Date(dateTimeStr); if (dateTime == null) return null;
        var month = ('0' + (dateTime.getMonth() + 1)).slice(-2);
        return month;
    }

    static getYear(dateTimeStr) {
        if (dateTimeStr == null) return null; var dateTime = new Date(dateTimeStr); if (dateTime == null) return null;
        var year = dateTime.getFullYear();
        return year;
    }
}