#!/bin/sh

# Change path to the root folder of the generated code.
cd ../openapi-2.0

branch_name="automation-pipeline-test-remote"
release_note="Automation pipeline script update"
git_branch=`git symbolic-ref --short HEAD`
current_branch=${git_branch}

# Sets the new remote
git_remote=`git remote`
if [ "$git_remote" = "" ]; then # git remote not defined

    if [ "$Automation_Pipeline_GIT_Token" = "" ]; then
        echo "[INFO] \$GIT_TOKEN (environment variable) is not set. Using the git credential in your environment."
        git remote add origin https://github.com/${GIT_USER_ID}/${GIT_REPO_ID}.git
    else
        git remote add origin https://${GIT_USER_ID}:${Automation_Pipeline_GIT_Token}@github.com/${GIT_USER_ID}/${GIT_REPO_ID}.git
    fi

fi

branch_exists=`git ls-remote --heads https://github.com/${GIT_USER_ID}/${GIT_REPO_ID} | grep $branch_name`
if [ "$branch_exists" = "" ]; then
    echo "[INFO] Branch does not exist. Creating branch $branch_name"
    git checkout master
    git pull origin master
    git checkout -b $branch_name
else
    echo "[INFO] Branch exists"
    git checkout $branch_name
    git pull origin $branch_name
fi

# Adds the files in the local repository and stages them for commit.
git add .

# Commits the tracked changes and prepares them to be pushed to a remote repository.
git commit -m "$release_note"

# Pushes the changes in the local repository up to the remote repository
echo "Git pushing to https://github.com/${GIT_USER_ID}/${GIT_REPO_ID}.git"
git push origin $branch_name 2>&1 | grep -v 'To https'

git checkout $current_branch