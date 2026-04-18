function setApiRequest(elem) {
    const checkboxInputs = document.querySelectorAll("tr input");
    const apiRequestText = document.getElementById("apiRequestText");
    const requestUrl = document.getElementById("apiRequestInput").value;
    const checkedInputs = {};
    let parameters = "";
    for (let input of checkboxInputs) {
        // ensure only one checkbox in each class is checked
        if (input.name == elem.name && input != elem) {
            input.checked = false;
        }

        if (input.checked) {
            checkedInputs[input.name] = input.value;
            parameters += `&${input.name}=${input.value}`;
        }
    }
    apiRequestText.value = requestUrl + parameters;
    fetchApiResults(checkedInputs);
}

async function fetchApiResults(inputs) {
    try {
        const resp = await fetch("/", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify(inputs)
        });
        const data = await resp.json();
        const formattedJson = JSON.stringify(data, null, 4);
        document.getElementById("jsonDisplay").innerText = formattedJson;
    }
    catch (e) {
        console.log(e.message);
    }
}