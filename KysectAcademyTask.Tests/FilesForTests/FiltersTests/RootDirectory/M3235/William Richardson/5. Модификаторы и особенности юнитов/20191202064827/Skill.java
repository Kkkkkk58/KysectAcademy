package game.base.unit.skills;

import game.base.exceptions.SkillCanNotUse;

public abstract class Skill {
    protected String title;
    protected Double countOfUsage;  // Double just for infinity

    protected Skill(String title, Double countOfUsage) {
        this.title = title;
        this.countOfUsage = countOfUsage;
    }

    public String getTitle() {
        return title;
    }

    public Double getCountOfUsage() {
        return countOfUsage;
    }

    protected void checkUsage() throws SkillCanNotUse {
        if (countOfUsage <= 0) {
            throw new SkillCanNotUse("You can't use this skill");
        }
    }
}
