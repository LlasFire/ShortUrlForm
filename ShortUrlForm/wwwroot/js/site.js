let parent = document.getElementById("parentForLink")
let links;

window.addEventListener('load', () => {
    links = document.getElementsByClassName("shortlink");
});

parent.addEventListener('click', (event) => {

    const target = event.target;

    if (target.className === "shortlink") {

        for (let i = 0; i < links.length; i++) {

            if (target === links[i]) {

                link = links[i];

                CountLinkActivites(link.id)

                break;
            }
        }
    }
});

function CountLinkActivites(id) {
    return fetch(`/Home/CounterClickLink?id=${id}`, { method: 'POST' });
}