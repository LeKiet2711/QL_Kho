<script>
    document.addEventListener('DOMContentLoaded', function () {
        const chatbox = document.getElementById('chatbox');
    const toggleButton = document.getElementById('toggleButton');
    const sendButton = document.getElementById('sendButton');
    const chatInput = document.getElementById('chatInput');

        toggleButton.addEventListener('click', () => {
        chatbox.classList.toggle('open');
        });

        sendButton.addEventListener('click', async () => {
            const prompt = chatInput.value;
    if (prompt) {
                const response = await fetch('/api/openai', {
        method: 'POST',
    headers: {
        'Content-Type': 'application/json'
                    },
    body: JSON.stringify({prompt})
                });
    const data = await response.json();
    alert(data.response);
            }
        });
    });
</script>
