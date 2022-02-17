

class AbonementPrivateConnectionsToUserFactory {

    create(divBlock, abonementLiteWithPrivateConnectionToUserViewModels, delegateFunction = null) {
        divBlock.innerHTML = "";
        var tableAbonementPrivateConnections;
        var tableBody;
        if (abonementLiteWithPrivateConnectionToUserViewModels.length <= 0) return;

        tableAbonementPrivateConnections = document.createElement("table");
        tableAbonementPrivateConnections.className = "table table-striped table-bordered mb30";
        var thead = document.createElement("thead");
        var tr = document.createElement("tr");
        var thID = document.createElement("th"); thID.innerHTML = "ID";
        var thName = document.createElement("th"); thName.innerHTML = "Наименование";
        var thIsPrivate = document.createElement("th"); thIsPrivate.innerHTML = "Статус приватности";
        var thStatus = document.createElement("th"); thStatus.innerHTML = "Статус";
        tr.appendChild(thID);
        tr.appendChild(thName);
        tr.appendChild(thIsPrivate);
        tr.appendChild(thStatus);
        thead.appendChild(tr);
        tableAbonementPrivateConnections.appendChild(thead);

        tableBody = document.createElement("tbody");

        for (var i = 0; i < abonementLiteWithPrivateConnectionToUserViewModels.length; i++) {
            tableBody.appendChild(this._buildAbonementPrivateConnectionToUser(abonementLiteWithPrivateConnectionToUserViewModels[i]));
        }
        tableAbonementPrivateConnections.appendChild(tableBody);
        divBlock.appendChild(tableAbonementPrivateConnections);
    }
    _buildAbonementPrivateConnectionToUser(abonementLiteWithPrivateConnectionToUserViewModel) {
        var tr = document.createElement("tr");
        var tdID = document.createElement("td"); tdID.innerHTML = abonementLiteWithPrivateConnectionToUserViewModel["id"];
        var tdName = document.createElement("td"); tdName.innerHTML = abonementLiteWithPrivateConnectionToUserViewModel["name"];

        var fontIsPrivate = document.createElement("font");
        fontIsPrivate.innerHTML = (abonementLiteWithPrivateConnectionToUserViewModel["isPrivate"] == 1 ? "Да" : "Нет");
        fontIsPrivate.className = (abonementLiteWithPrivateConnectionToUserViewModel["isPrivate"] == 1 ? "success" : "danger");
        var tdIsPrivate = document.createElement("td"); tdIsPrivate.appendChild(fontIsPrivate);

        var tdStatus = document.createElement("td"); tdStatus.appendChild(this._buildAbonementPrivateConnectionToUserStatus(abonementLiteWithPrivateConnectionToUserViewModel));

        tr.appendChild(tdID);
        tr.appendChild(tdName);
        tr.appendChild(tdIsPrivate);
        tr.appendChild(tdStatus);
        return tr;
    }
    _buildAbonementPrivateConnectionToUserStatus(abonementLiteWithPrivateConnectionToUserViewModel) {
        var select = document.createElement("select");
        select.className = "form-control";
        select.onchange = function () {
            _userCardUpdateConnectionAbonementPrivateToUser(abonementLiteWithPrivateConnectionToUserViewModel["id"], this.value);
        }
        var option0 = document.createElement("option"); option0.value = 0; option0.innerHTML = "Нет";
        option0.selected = (abonementLiteWithPrivateConnectionToUserViewModel["status"] == 0 ? "selected" : "");
        var option1 = document.createElement("option"); option1.value = 1; option1.innerHTML = "Да";
        option1.selected = (abonementLiteWithPrivateConnectionToUserViewModel["status"] == 1 ? "selected" : "");
        select.appendChild(option0);
        select.appendChild(option1);
        return select;
    }
}