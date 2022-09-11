var constructionApiToken;

function getToken() {
    // Used by "WorkSheetDetail.cshtml" page.
    return new Promise(function (resolve, reject) {
        var Request = $.ajax({
            url: 'https://siriwebapi.sansiri.com/homecare_api/api/login/getTokenCode',
            type: 'GET',
            contentType: "application/json; charset=utf-8"
        });
        Request.done(function (data) {
            resolve(data);
        });
        Request.fail(function (jqXHR, textStatus, error) {
            reject(error);
        });
    });
}

async function getTokenAsync() {
    var response = await fetch('https://siriwebapi.sansiri.com/homecare_api/api/login/getTokenCode', {
        method: 'GET',
        cache: 'no-cache'
    });
    var json = await response.json();
    constructionApiToken = json.response;
}

async function ajaxGet(url, callback = null) {
    if (!constructionApiToken) {
        await getTokenAsync();
    }
    var response = await fetch(url, {
        method: 'GET',
        cache: 'no-cache',
        headers: {
            Authorization: 'Bearer ' + constructionApiToken
        },
    });
    var json = await response.json();
    if (!response.ok) {
        if (json.statusCode === 401) {
            await getTokenAsync();
            return ajaxGet(url, callback);
		}
		page.loaded();
		pleaseWaitDialogClose();
        if (json) {
            modalWaring(json.messageTh || json.message || 'เกิดข้อผิดพลาดในระบบ กรุณาติดต่อผู้ดูแลระบบ');
        }
        if (callback) {
            return callback(false);
        }
        return false;
    }
    if (callback) {
        return callback(json);
    }
    return json;
}

var get = async function (url) {
    try {
        return url ? (await ajaxGet(url)).data : undefined;
    } catch (e) {
        return undefined;
    }
}

async function ajaxPost(url, body, callback = null) {
    if (!constructionApiToken) {
        await getTokenAsync();
    }
    var response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'Authorization': 'Bearer ' + constructionApiToken
        },
        body: JSON.stringify(body)
    });
    var json = await response.json();
    if (!response.ok) {
        if (json.statusCode === 401) {
            await getTokenAsync();
            return ajaxPost(url, body, callback);
        }
		page.loaded();
		pleaseWaitDialogClose();
        if (json) {
            modalWaring(json.messageTh || json.message || 'เกิดข้อผิดพลาดในระบบ กรุณาติดต่อผู้ดูแลระบบ');
        }
        if (callback) {
            return callback(false);
        }
        return false;
    }
    if (callback) {
        return callback(json);
    }
    return json;
}

async function ajaxPut(url, body, callback = null) {
    if (!constructionApiToken) {
        await getTokenAsync();
    }
    var response = await fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'Authorization': 'Bearer ' + constructionApiToken
        },
        body: JSON.stringify(body)
    });
    var json = await response.json();
    if (!response.ok) {
        if (json.statusCode === 401) {
            await getTokenAsync();
            return ajaxPut(url, body, callback);
        }
		page.loaded();
		pleaseWaitDialogClose();
        if (json) {
            modalWaring(json.messageTh || json.message || 'เกิดข้อผิดพลาดในระบบ กรุณาติดต่อผู้ดูแลระบบ');
        }
        if (callback) {
            return callback(false);
        }
        return false;
    }
    if (callback) {
        return callback(json);
    }
    return json;
}

async function ajaxDelete(url, body, callback = null) {
    if (!constructionApiToken) {
        await getTokenAsync();
    }
    var response = await fetch(url, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'Authorization': 'Bearer ' + constructionApiToken
        },
        body: JSON.stringify(body)
    });
    var json = await response.json();
    if (!response.ok) {
        if (json.statusCode === 401) {
            await getTokenAsync();
            return ajaxDelete(url, body, callback);
        }
		page.loaded();
		pleaseWaitDialogClose();
        if (json) {
            modalWaring(json.messageTh || json.message || 'เกิดข้อผิดพลาดในระบบ กรุณาติดต่อผู้ดูแลระบบ');
        }
        if (callback) {
            return callback(false);
        }
        return false;
    }
    if (callback) {
        return callback(json);
    }
    return json;
}

async function ajaxPostFormData(url, body, callback = null) {
    if (!constructionApiToken) {
        await getTokenAsync();
    }
    var response = await fetch(url, {
        method: 'POST',
        headers: {           
            'Authorization': 'Bearer ' + constructionApiToken
        },
        processData: false,
        contentType: false,
        body: body
    });
    var json = await response.json();
    if (!response.ok) {
        if (json.statusCode === 401) {
            await getTokenAsync();
            return ajaxPost(url, body, callback);
        }
        page.loaded();
        pleaseWaitDialogClose();
        if (json) {
            modalWaring(json.messageTh || json.message || 'เกิดข้อผิดพลาดในระบบ กรุณาติดต่อผู้ดูแลระบบ');
        }
        if (callback) {
            return callback(false);
        }
        return false;
    }
    if (callback) {
        return callback(json);
    }
    return json;
}

async function ajaxGetFile(url, callback = null) {
    if (!constructionApiToken) {
        await getTokenAsync();
    }
    var response = await fetch(url, {
        method: 'GET',
        cache: 'no-cache',
        headers: {
            Authorization: 'Bearer ' + constructionApiToken
        },
    });
    
    if (!response.status === 200) {
        var json = await response.json();

        if (json.statusCode === 401) {
            await getTokenAsync();
            return ajaxGet(url, callback);
        }
        page.loaded();
        pleaseWaitDialogClose();
        if (json) {
            modalWaring(json.messageTh || json.message || 'เกิดข้อผิดพลาดในระบบ กรุณาติดต่อผู้ดูแลระบบ');
        }
        if (callback) {
            return callback(false);
        }
        return false;
    }
    response.headers.forEach(v => {
        console.log(v)
    })
    if (callback) {
        return callback(response);
    }
    return response
}
$('.modal').on('hidden.bs.modal', function () {
    $('body').css('padding-right', '0');
});