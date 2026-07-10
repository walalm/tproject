document.addEventListener('DOMContentLoaded', function () {
    const taskLists = document.querySelectorAll('.task-list');

    taskLists.forEach(function (list) {
        new Sortable(list, {
            group: 'tasks',
            animation: 150,
            ghostClass: 'task-ghost',
            dragClass: 'task-dragging',
            onEnd: function (evt) {
                const taskEl = evt.item;
                const taskId = parseInt(taskEl.dataset.taskId, 10);
                const columnEl = evt.to.closest('.board-column');
                const columnId = parseInt(columnEl.dataset.columnId, 10);
                const newPosition = evt.newIndex;

                fetch('/api/tasks/move', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({
                        taskId: taskId,
                        columnId: columnId,
                        newPosition: newPosition
                    })
                }).then(function (response) {
                    if (!response.ok) {
                        console.error('Failed to move task');
                    }
                }).catch(function (err) {
                    console.error('Error moving task:', err);
                });
            }
        });
    });

    document.querySelectorAll('.add-task-form').forEach(function (form) {
        form.addEventListener('submit', function (e) {
            const input = form.querySelector('input[name="Title"]');
            if (!input.value.trim()) {
                e.preventDefault();
            }
        });
    });
});
