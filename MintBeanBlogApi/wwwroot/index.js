const api = {
    create: async (title, content) => {
        const post = { title, content }

        return await fetch('posts', {
            method: 'POST',
            body: JSON.stringify(post),
            headers: { 'Content-Type': 'application/json' },
        })
    },
    get: async (published) => {
        return await fetch(`posts?published=${published}`)
            .then(res => res.json())
    },
    update: async (id, title, content, published) => {
        const post = { id, title, content, published }

        return await fetch('posts', {
            method: 'PUT',
            body: JSON.stringify(post),
            headers: { 'Content-Type': 'application/json' },
        })
    },
    delete: async (id) => {
        return await fetch(`posts/${id}`, { method: 'DELETE' })
    }
}

async function initialise() {
    const createPostButton = document.getElementById('create-post-button')
    createPostButton.addEventListener('click', createPost)

    await loadPosts()
}

async function loadPosts() {
    const publishedPosts = await api.get(true)
    const unpublishedPosts = await api.get(false)

    await renderPostList('published-posts', publishedPosts)
    await renderPostList('unpublished-posts', unpublishedPosts)
}

async function createPost() {
    const titleInput = document.getElementById('title-input')
    const contentInput = document.getElementById('content-input')

    await api.create(titleInput.value, contentInput.value)

    titleInput.value = ''
    contentInput.value = ''

    await loadPosts()
}

async function renderPostList(containerId, posts) {
    const container = document.getElementById(containerId)
    container.innerHTML = '' // clear list

    posts.forEach(post => {
        const listItem = document.createElement('li')

        const publishButton = post.published ? '' : '<button class="publish-button">Publish</button>'
        const deleteButton = '<button class="delete-button">Delete</button>'

        listItem.innerHTML = `${post.title} - ${post.content} ${publishButton} ${deleteButton}`
        listItem.setAttribute('data-post-id', post.id)

        if (publishButton) listItem.querySelector('.publish-button').addEventListener('click', () => updatePost(post.id, post.title, post.content, true))
        listItem.querySelector('.delete-button').addEventListener('click', () => deletePost(post.id))

        container.append(listItem)
    })
}

async function updatePost(id, title, content, published) {
    await api.update(id, title, content, published)
    await loadPosts()
}

async function deletePost(id) {
    await api.delete(id)
    await loadPosts()
}

initialise()
