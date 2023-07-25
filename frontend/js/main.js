window.addEventListener("DOMContentLoaded", (event) => {
    getVisitorCount().then(count => {
        document.getElementById("counter").innerText = count;
    }).catch(error => {
        console.log(error);
    });
});

const functionAPIUrl = "https://getvisitorcount.azurewebsites.net/api/GetVisitorCount?code=qg5UPd8h2ye-qiWxeOqobFECzmJGpOpfxsgbqFgmH9WwAzFuzvxdhg==";
const localFunctionAPI = 'http://localhost:7071/api/GetVisitorCount';
const prvFunctnAPI = "getviewscounter.privatelink.azurewebsites.net";

const getVisitorCount = async () => {
    try {
        const response = await fetch(prvFunctnAPI);
        if (response.ok) {
            const data = await response.json();
            console.log("Website called function API.");
            return data.count;
        } else {
            throw new Error("Failed to fetch visitor count.");
        }
    } catch (error) {
        console.log(error);
        throw error;
    }
}