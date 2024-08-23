

document.querySelectorAll('.remove-role-btn').forEach(button => {
    button.addEventListener('click', function (event) {
        event.preventDefault();  // Prevent the default form submission
        const form = this.closest('form');
        const userId = form.querySelector('input[name="UserId"]').value;

        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, remove it!'
        }).then((result) => {
            if (result.isConfirmed) {

                fetch(`/Admin/User/GetRoleCount?userId=${userId}`)
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok');
                        }
                        return response.json();
                    })
                    .then(data => {
                        if (data.roleCount > 1) {
                            form.submit();
                        } else {
                            Swal.fire('Error', 'Cannot remove last role.', 'error');
                        }
                    })
                    .catch(error => {
                        console.error('Error:', error);
                        Swal.fire('Error', 'An error occurred while checking roles.', 'error');
                    });

            }
        });
    });
});
