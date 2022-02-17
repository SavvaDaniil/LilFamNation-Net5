


class TableVisitLessonFactory {

    create(divBlock, visitLessonHeaderDates, visitLessonUserDatas, visitLessonRowPurchaseSumm, visitLessonRowUserCount, visitLessonRowTeacherSalary, delegateFunction = null) {

        var table = document.createElement("table");
        table.className = "table table-bordered mb30 table-hover";

        var tHead = document.createElement("thead");
        var trHead = document.createElement("tr");

        var td;


        td = document.createElement("th");td.innerHTML = "id";
        trHead.appendChild(td);

        td = document.createElement("th"); td.innerHTML = "Ученик";
        trHead.appendChild(td);

        visitLessonHeaderDates.forEach(visitLessonHeaderDate => {
            trHead.appendChild(this._buildTableHeaderDate(visitLessonHeaderDate));
        });
        td = document.createElement("th");
        trHead.appendChild(td);
        tHead.appendChild(trHead);
        table.appendChild(tHead);

        var tBody = document.createElement("tbody");
        /*
        visitLessonUserDatas.forEach(visitLessonUserData => {
            tBody.appendChild(this._buildRowVisitLessonUserData(visitLessonUserData), delegateFunction);
        });
        */
        for (var i = 0; i < visitLessonUserDatas.length; i++) {
            tBody.appendChild(this._buildRowVisitLessonUserData(visitLessonUserDatas[i], delegateFunction));
        }

        tBody.appendChild(this._buildRowPurchaseSumm(visitLessonRowPurchaseSumm));
        tBody.appendChild(this._buildRowUserCount(visitLessonRowUserCount));
        tBody.appendChild(this._buildRowTeacherSalary(visitLessonRowTeacherSalary));


        table.appendChild(tBody);



        divBlock.appendChild(table);
    }

    _buildRowPurchaseSumm(visitLessonRowPurchaseSumm) {
        var tr = document.createElement("tr");
        var td;
        td = document.createElement("td");
        tr.appendChild(td);
        td = document.createElement("td");
        var p = document.createElement("p");
        p.className = "statistik-header";
        p.innerHTML = "Сумма с покупок";
        var br = document.createElement("br"); p.appendChild(br);
        var spanDesc = document.createElement("span");
        spanDesc.className = "desc";
        spanDesc.innerHTML = "Щелкните на нужной дате, чтобы получить историю продаж";
        p.appendChild(spanDesc);
        td.appendChild(p);
        tr.appendChild(td);

        var visitLessonSumms = visitLessonRowPurchaseSumm["visitLessonPurchaseSumms"];
        visitLessonSumms.forEach(visitLessonPurchaseSumm => {
            var tdSumm = document.createElement("td");
            tdSumm.className = "statistik-block";
            tdSumm.onclick = function () {
                loadModalListOfPurchasesAbonementByDate(visitLessonPurchaseSumm["dateOfLesson"]);
            }
            if (!visitLessonPurchaseSumm["isAlreadyCount"]) tdSumm.innerHTML = visitLessonPurchaseSumm["summ"];
            tr.appendChild(tdSumm);
        });

        td = document.createElement("td");
        td.className = "statistik-header";
        td.innerHTML = "Итого: " + visitLessonRowPurchaseSumm["summFromAll"];
        tr.appendChild(td);

        return tr;
    }


    _buildRowUserCount(visitLessonRowUserCount) {
        var tr = document.createElement("tr");
        var td;
        td = document.createElement("td");
        tr.appendChild(td);
        td = document.createElement("td");
        var p = document.createElement("p");
        p.className = "statistik-header";
        p.innerHTML = "Количество учеников";
        var br = document.createElement("br"); p.appendChild(br);
        var spanDesc = document.createElement("span");
        spanDesc.className = "desc";
        spanDesc.innerHTML = "Щелкните на нужной дате, чтобы получить список учеников";
        p.appendChild(spanDesc);
        td.appendChild(p);
        tr.appendChild(td);

        var visitLessonUserCounts = visitLessonRowUserCount["visitLessonUserCounts"];
        visitLessonUserCounts.forEach(visitLessonUserCount => {
            var tdCount = document.createElement("td");
            tdCount.className = "statistik-block";
            tdCount.onclick = function () {
                loadModalListOfVisitsByDate(visitLessonUserCount["dateOfLesson"], visitLessonUserCount["id_of_dance_group"], visitLessonUserCount["id_of_dance_group_day_of_week"]);
            }
            tdCount.innerHTML = visitLessonUserCount["count"];
            tr.appendChild(tdCount);
        });

        td = document.createElement("td");
        td.className = "statistik-header";
        td.innerHTML = "Итого: " + visitLessonRowUserCount["countAll"];
        tr.appendChild(td);

        return tr;
    }

    _buildRowTeacherSalary(visitLessonRowTeacherSalary) {
        var tr = document.createElement("tr");
        var td;
        td = document.createElement("td");
        tr.appendChild(td);
        td = document.createElement("td");
        var p = document.createElement("p");
        p.className = "statistik-header";
        p.innerHTML = "Зарплата педагогу";
        var br = document.createElement("br"); p.appendChild(br);
        var spanDesc = document.createElement("span");
        spanDesc.className = "desc";
        spanDesc.innerHTML = "Щелкните на нужной дате, чтобы получить подробную информацию";
        p.appendChild(spanDesc);
        td.appendChild(p);
        tr.appendChild(td);

        var visitLessonTeacherSalaries = visitLessonRowTeacherSalary["visitLessonTeacherSalaries"];
        visitLessonTeacherSalaries.forEach(visitLessonTeacherSalary => {
            var tdCount = document.createElement("td");
            tdCount.className = "statistik-block";
            tdCount.onclick = function () {
                loadModalListOfVisitsByDate(visitLessonTeacherSalary["dateOfLesson"], visitLessonTeacherSalary["id_of_dance_group"], visitLessonTeacherSalary["id_of_dance_group_day_of_week"]);
            }
            tdCount.innerHTML = visitLessonTeacherSalary["summ"];
            tr.appendChild(tdCount);
        });

        td = document.createElement("td");
        td.className = "statistik-header";
        td.innerHTML = "Итого: " + visitLessonRowTeacherSalary["summAll"];
        tr.appendChild(td);

        return tr;
    }









    _buildTableHeaderDate(visitLessonHeaderDate) {
        var td = document.createElement("th"); //td.innerHTML = visitLessonHeaderDate["dateDayMonth"];
        td.className = "visitLessonHeaderDate";
        if (visitLessonHeaderDate["isReplacementExistForLesson"]) td.classList.add("replacement-exist");
        var p = document.createElement("p");
        p.innerHTML = visitLessonHeaderDate["dateDayMonth"];
        var br = document.createElement("br");
        p.appendChild(br);
        var spanTime = document.createElement("span");
        spanTime.innerHTML = visitLessonHeaderDate["timeFrom"];
        p.appendChild(spanTime);
        td.appendChild(p);

        td.onclick = function () {
            loadModalTeacherReplacement(visitLessonHeaderDate["dateYearMonthDay"], visitLessonHeaderDate["id_of_dance_group_day_of_week"]);
        }

        return td;
    }

    _buildRowVisitLessonUserData(visitLessonUserData, delegateFunction = null) {
        var tr = document.createElement("tr");
        var tdId = document.createElement("td"); tdId.innerHTML = visitLessonUserData["id_of_user"]; tr.appendChild(tdId);
        var tdFio = document.createElement("td"); tdFio.innerHTML = visitLessonUserData["fio"]; tr.appendChild(tdFio);

        /*
        const userVisitsByDate = visitLessonUserData["userVisitsByDate"];
        for (const [keyDate, valueVisitLessonUserVisitStatuses] of Object.entries(userVisitsByDate)) {
            tr.appendChild(this._buildTdUserVisitByDate(visitLessonUserData["id_of_user"], keyDate, valueVisitLessonUserVisitStatuses));
        }
        */
        const danceGroupLessonUserVisitsDatas = visitLessonUserData["danceGroupLessonUserVisitsDatas"];
        danceGroupLessonUserVisitsDatas.forEach(danceGroupLessonUserVisitsData => {
            tr.appendChild(
                this._buildTdUserVisitByDate(visitLessonUserData["id_of_user"],
                    danceGroupLessonUserVisitsData["danceGroupLesson"]["dateOfLesson"],
                    danceGroupLessonUserVisitsData["danceGroupLesson"]["id_of_dance_group_day_of_week"],
                    danceGroupLessonUserVisitsData["visitLessonUserVisitStatuses"],
                    delegateFunction
                ));
        });



        var tdDisconnectUserFromDanceGroup = document.createElement("td");
        tdDisconnectUserFromDanceGroup.className = "disconnectUserFromDanceGroup";
        var aDisconnectUserFromDanceGroup = document.createElement("a"); aDisconnectUserFromDanceGroup.href = "#";
        aDisconnectUserFromDanceGroup.innerHTML = "Отключить от группы";
        aDisconnectUserFromDanceGroup.onclick = function () {
            updateConnectUserToDanceGroup(visitLessonUserData["id_of_user"], 0);
        }
        tdDisconnectUserFromDanceGroup.appendChild(aDisconnectUserFromDanceGroup);
        tr.appendChild(tdDisconnectUserFromDanceGroup);

        return tr;
    }

    _buildTdUserVisitByDate(id_of_user, date, id_of_dance_group_day_of_week, visitLessonUserVisitStatuses, delegateFunction = null) {
        var td = document.createElement("td");
        td.className = "visitLessonUserVisitStatus";
        if (visitLessonUserVisitStatuses.length > 0) {
            visitLessonUserVisitStatuses.forEach(visitLessonUserVisitStatus => {
                var p = document.createElement("p");
                p.className = "visitLessonUserData";
                var a = document.createElement("a");
                a.href = "#";
                a.innerHTML = visitLessonUserVisitStatus["visitLeft"];
                p.appendChild(a);
                td.appendChild(p);
            })
        }
        //console.log((delegateFunction == null ? "delegateFunction is null" : "delegateFunction is not null"));
        td.onclick = function () {
           // if (delegateFunctionAfterAddVisit == null) console.log("delegateFunctionAfterAddVisit == null");
            openModalBuyVisit(id_of_user, ID_OF_DANCE_GROUP, date, id_of_dance_group_day_of_week, delegateFunction);
        }
        return td;
    }
}