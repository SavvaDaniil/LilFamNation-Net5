

class DanceGroupConnectionsToUserAdminFactory {

    create(divBlock, danceGroupWithConnectionToUserLiteViewModels, delegateFunction = null) {
        divBlock.innerHTML = "";
        var tableDanceGroupConnections;
        var tableBody;
        if (danceGroupWithConnectionToUserLiteViewModels.length <= 0) return;

        tableDanceGroupConnections = document.createElement("table");
        tableDanceGroupConnections.className = "table table-striped table-bordered mb30";
        var thead = document.createElement("thead");
        var tr = document.createElement("tr");
        var thID = document.createElement("th"); thID.innerHTML = "ID";
        var thName = document.createElement("th"); thName.innerHTML = "Наименование";
        var thStatus = document.createElement("th"); thStatus.innerHTML = "Статус";
        tr.appendChild(thID);
        tr.appendChild(thName);
        tr.appendChild(thStatus);
        thead.appendChild(tr);
        tableDanceGroupConnections.appendChild(thead);

        tableBody = document.createElement("tbody");

        for (var i = 0; i < danceGroupWithConnectionToUserLiteViewModels.length; i++) {
            tableBody.appendChild(this._buildDanceGroupConnectionToUser(danceGroupWithConnectionToUserLiteViewModels[i]));
        }
        tableDanceGroupConnections.appendChild(tableBody);
        divBlock.appendChild(tableDanceGroupConnections);
    }
    _buildDanceGroupConnectionToUser(danceGroupWithConnectionToUserLiteViewModel) {
        var tr = document.createElement("tr");
        var tdID = document.createElement("td"); tdID.innerHTML = danceGroupWithConnectionToUserLiteViewModel["id"];
        var tdName = document.createElement("td"); tdName.innerHTML = danceGroupWithConnectionToUserLiteViewModel["name"];
        var tdStatus = document.createElement("td"); tdStatus.appendChild(this._buildDanceGroupConnectionToUserStatus(danceGroupWithConnectionToUserLiteViewModel));

        tr.appendChild(tdID);
        tr.appendChild(tdName);
        tr.appendChild(tdStatus);
        return tr;
    }
    _buildDanceGroupConnectionToUserStatus(danceGroupWithConnectionToUserLiteViewModel) {
        var select = document.createElement("select");
        select.className = "form-control";
        select.onchange = function () {
            _userCardUpdateDanceGroupConnectionToUserAdmin(danceGroupWithConnectionToUserLiteViewModel["id"], this.value);
        }
        var option0 = document.createElement("option"); option0.value = 0; option0.innerHTML = "Нет";
        option0.selected = (danceGroupWithConnectionToUserLiteViewModel["status"] == 0 ? "selected" : "");
        var option1 = document.createElement("option"); option1.value = 1; option1.innerHTML = "Да";
        option1.selected = (danceGroupWithConnectionToUserLiteViewModel["status"] == 1 ? "selected" : "");
        select.appendChild(option0);
        select.appendChild(option1);
        return select;
    }

}